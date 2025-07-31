using CadastroEmpresa.Data;
using CadastroEmpresa.Dto;
using CadastroEmpresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CadastroEmpresa.Services.UsuarioService.UsuarioService
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using var hmac = new HMACSHA512();
            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public async Task<bool> DeletarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if(usuario == null)
                return false;
            
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public string GerarToken(UsuarioModel usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(6),
                signingCredentials: creds
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponse> LoginAsync(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLoginDto.Email);

            if (usuario == null || !VerificaSenhaHash(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                return new AuthResponse
                {
                    Autenticado = false,
                    Mensagem = "Email ou senha inválidos.",
                    Token = string.Empty
                };
            }

            var token = GerarToken(usuario);

            return new AuthResponse
            {
                Autenticado = true,
                Mensagem = "Login realizado com sucesso.",
                Token = token
            };
        }


        public async Task<UsuarioModel> ObterUsuarioPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<AuthResponse> RegistrarAsync(UsuarioRegisterDto usuarioRegisterDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuarioRegisterDto.Email))
            {
                return new AuthResponse
                {
                    Autenticado = false,
                    Mensagem = "Usuário já cadastrado com esse e-mail.",
                    Token = string.Empty
                };
            }

            CriarSenhaHash(usuarioRegisterDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioModel
            {
                Nome = usuarioRegisterDto.Nome,
                Email = usuarioRegisterDto.Email,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var token = GerarToken(usuario);

            return new AuthResponse
            {
                Autenticado = true,
                Mensagem = "Usuário cadastrado com sucesso.",
                Token = token
            };
        }


        public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using var hmac = new HMACSHA512(senhaSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return computedHash.SequenceEqual(senhaHash);
        }
    }
}
