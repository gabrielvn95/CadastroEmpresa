using CadastroEmpresa.Dto;
using CadastroEmpresa.Models;

namespace CadastroEmpresa.Services
{
    public interface IUsuarioInterface
    {
        Task<AuthResponse> RegistrarAsync(UsuarioRegisterDto usuarioRegisterDto);
        Task<AuthResponse> LoginAsync(UsuarioLoginDto usuarioLoginDto);
        string GerarToken(UsuarioModel usuario);
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        Task<UsuarioModel> ObterUsuarioPorEmailAsync(string email);

        Task<bool> DeletarUsuario(int id);
    }
}
