using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresa.Dto
{
    public class UsuarioRegisterDto
    {
        [Required(ErrorMessage = "O nome e obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }
    }
}
