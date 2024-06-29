using WebApiFuncionarios.Models;

namespace WebApiFuncionarios.Service.FuncionarioService
{
    //Aqui é um contrato, todos os metodos estarao aqui
    public interface IFuncionarioInterface
    {
        //Aqui ficarao todos os metodos que iremos usar no Controller


        //Task porque é uma boa pratica usar metodos assincronos
        //Ele deve retornar um service response que tem dentro dele uma lista de funcionariosModel
        Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios();
        Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario);
        Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id);
        Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario);
        Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id);
        Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id);
    }
}
