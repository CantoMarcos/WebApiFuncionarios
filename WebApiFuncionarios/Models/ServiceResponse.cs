namespace WebApiFuncionarios.Models
{
    public class ServiceResponse<T> //Recebe dados genéricos
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty; //começa sendo uma string vazia
        public bool Sucesso { get; set; } = true; //ja começa em verdadeiro. Caso dê algum erro na requisição mudamos o valor para false
    }
}
