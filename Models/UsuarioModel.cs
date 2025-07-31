using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresa.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] SenhaHash { get; set; }

        [Required]
        public byte[] SenhaSalt { get; set; }

        public ICollection<EmpresaModel> Empresas { get; set; } 

    }
}
