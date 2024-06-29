using Microsoft.EntityFrameworkCore;
using WebApiFuncionarios.DataContext;
using WebApiFuncionarios.Models;

namespace WebApiFuncionarios.Service.FuncionarioService
{
    //O funcionarioService pega dados do ifuncionarioInterface (herança)
    public class FuncionarioService : IFuncionarioInterface
    {
        //Aqui ficará a logica dos metodos que criamos na Interface
        //O Controller so faz as requisições do que tem aqui

        //Injeççao de Dependencia para receber dentro do contrutor o que queremos utilizar, ou seja o que tem no banco de dados
        private readonly ApplicationDbContext _context;
        //quando usar o _context teremos acesso a todo os bancos que foram criados (tabelas do banco)
        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            //precisamos retornar algo do tipo <ServiceResponse<List<FuncionarioModel>>
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                //Colocar os dados dentro do ServiceResponse que iremos retornar
                //no objeto serviceResponse na area de Dados iremos pegar tudo que tem na tabela funcionarios 
                //Do banco de dados e apresentar como lista
                serviceResponse.Dados = _context.Funcionarios.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if(novoFuncionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "informar Dados";
                    serviceResponse.Sucesso=false;

                    return serviceResponse;
                }
                _context.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuario nao localizado";
                    serviceResponse.Sucesso = false;
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();
            try
            {
                //Nesse caso eu vou pegar o Funcionario no banco de dados em que x.Id (ele é um objeto do tipo FuncionarioModel)
                //e atribuir esse objeto a funcionario. O id dele tem que ser igual ao que eu recebo do método

                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuario nao localizado";
                    serviceResponse.Sucesso = false;

                }
                serviceResponse.Dados = funcionario;
            }
            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id  == id);   

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuario nao localizado";
                    serviceResponse.Sucesso = false;
                }

                funcionario.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            try
            {
                //Usar o AsNoTracking no caso da edição de dados no banco
                FuncionarioModel funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id ==editadoFuncionario.Id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuario não localizado";
                    serviceResponse.Sucesso = false;
                }
                funcionario.Nome = editadoFuncionario.Nome;
                funcionario.Sobrenome = editadoFuncionario.Sobrenome;
                funcionario.Departamento = editadoFuncionario.Departamento;
                funcionario.Turno = editadoFuncionario.Turno;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
