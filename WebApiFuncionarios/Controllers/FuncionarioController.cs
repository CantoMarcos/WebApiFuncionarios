using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFuncionarios.Models;
using WebApiFuncionarios.Service.FuncionarioService;

namespace WebApiFuncionarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        //Precisamos de uma injecao de dependencia para conseguir acessar o IFuncionarioInterface
        private readonly IFuncionarioInterface _iFuncionarioInterface;
        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _iFuncionarioInterface = funcionarioInterface;
        }

        [HttpGet]
        //o que iremos retornar? nesse caso um ActionResulte do tipo Lista de FuncionarioModel e que é assincrono(Task)
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarios()
        {
            return Ok(await _iFuncionarioInterface.GetFuncionarios());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = await _iFuncionarioInterface.GetFuncionarioById(id);

            return Ok(serviceResponse);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            return Ok(await _iFuncionarioInterface.CreateFuncionario(novoFuncionario));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> UpdateFuncionarios(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _iFuncionarioInterface.UpdateFuncionario(editadoFuncionario);

            return Ok(serviceResponse);

        }

        [HttpPut("inativaFuncionario")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> InativaFuncionarios(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _iFuncionarioInterface.InativaFuncionario(id);

            return Ok(serviceResponse);

        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> DeleteFuncionarios(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _iFuncionarioInterface.DeleteFuncionario(id);

            return Ok(serviceResponse);
        }




    }
}
