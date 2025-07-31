using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroEmpresa.Models
{
    public class EmpresaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(18)]
        public string Cnpj { get; set; }

        public string NomeEmpresarial { get; set; }

        public string NomeFantasia { get; set; }

        public string Situacao { get; set; }

        public string Abertura { get; set; }
      
        public string Tipo { get; set; }
       
        public string NaturezaJuridica { get; set; }

        public string AtividadePrincipal { get; set; }
       
        public string Logradouro { get; set; }
       
        public string  Numero { get; set; }

        public string Complemento { get; set; }
     
        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string Uf { get; set; }
       
        public string Cep { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }




    }
}
