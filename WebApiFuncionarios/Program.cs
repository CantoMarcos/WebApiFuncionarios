using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApiFuncionarios.DataContext;
using WebApiFuncionarios.Service.FuncionarioService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Quando fizer um injecao de dependencia da interface IfuncionarioInterface eu quero que seja utilizado
// os metodos que estao em funcionarioService
builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>();

// Configurar o DbContext com EnableRetryOnFailure
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 37)); // vers�o do MySQL
    options.UseMySql(connectionString, serverVersion, mysqlOptions =>
    {
        mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10, // N�mero m�ximo de tentativas
            maxRetryDelay: TimeSpan.FromSeconds(5), // Tempo m�ximo de atraso entre tentativas
            errorNumbersToAdd: null // Adicione n�meros de erro espec�ficos que devem disparar a resili�ncia, se necess�rio
        );
    });
});

// Configura��o do Kestrel para escutar em um endere�o IP espec�fico

/*builder.WebHost.UseKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Parse("192.168.1.18"), 5000); // IP e porta
    //Se eu quiser usar tambem o https
    //options.Listen(System.Net.IPAddress.Parse("192.168.1.18"), 5001, listenOptions =>
    //{
        //listenOptions.UseHttps();
    //});
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
