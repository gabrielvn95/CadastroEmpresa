using CadastroEmpresa.Data;
using CadastroEmpresa.Dto;
using CadastroEmpresa.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace CadastroEmpresa.Services
{
    public class EmpresaService : IEmpresaInterface
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public EmpresaService(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<EmpresaModel> CadastrarEmpresa(string cnpj, int usuarioId)
        {
            string cnpjLimpo = Regex.Replace(cnpj, @"\D", "");

            if (string.IsNullOrEmpty(cnpjLimpo) || cnpjLimpo.Length != 14)
                throw new ArgumentException("CNPJ inválido.");

            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpjLimpo}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var receita = JsonConvert.DeserializeObject<ReceitaWsResponse>(content);

            if (receita == null || receita.Status?.ToLower() != "ok")
                throw new Exception("Erro ao consultar CNPJ");

            var empresa = new EmpresaModel
            {
                Cnpj = cnpjLimpo,
                NomeEmpresarial = receita.Nome,
                NomeFantasia = receita.Fantasia,
                Situacao = receita.Situacao,
                Abertura = receita.Abertura,
                Tipo = receita.Tipo,
                NaturezaJuridica = receita.Natureza_juridica,
                AtividadePrincipal = receita.Atividade_principal.FirstOrDefault()?.Text,
                Logradouro = receita.Logradouro,
                Numero = receita.Numero,
                Complemento = receita.Complemento,
                Bairro = receita.Bairro,
                Municipio = receita.Municipio,
                Uf = receita.Uf,
                Cep = receita.Cep,
                UsuarioId = usuarioId
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<List<EmpresaModel>> ListarEmpresas(int usuarioId)
        {
            return await _context.Empresas
                .Where(e => e.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
