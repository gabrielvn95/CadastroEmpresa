using CadastroEmpresa.Models;

namespace CadastroEmpresa.Services
{
    public interface IEmpresaInterface
    {
        Task<EmpresaModel> CadastrarEmpresa(string cnpj, int usuarioId);
        Task<List<EmpresaModel>> ListarEmpresas(int usuarioId);
    }
}
