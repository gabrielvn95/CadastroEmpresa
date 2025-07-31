namespace CadastroEmpresa.Dto
{
    public class ReceitaWsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public string Situacao { get; set; }
        public string Abertura { get; set; }
        public string Tipo { get; set; }
        public string Natureza_juridica { get; set; }
        public List<AtividadePrincipal> Atividade_principal { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
    }

    public class AtividadePrincipal
    {
        public string Text { get; set; }
        public string Code { get; set; }
    }
}
