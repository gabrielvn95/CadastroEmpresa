namespace CadastroEmpresa.Dto
{
    public class AuthResponse
    {
        public bool Autenticado { get; set; }
        public string Mensagem { get; set; }
        public string Token { get; set; }
    }
}
