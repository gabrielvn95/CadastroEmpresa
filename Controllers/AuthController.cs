using CadastroEmpresa.Dto;
using CadastroEmpresa.Services.UsuarioService.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CadastroEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioService;

        public AuthController(IUsuarioInterface usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegisterDto usuarioRegisterDto)
        {
            var response = await _usuarioService.RegistrarAsync(usuarioRegisterDto);

            if (!response.Autenticado)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var response = await _usuarioService.LoginAsync(usuarioLoginDto);

            if (!response.Autenticado)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuarioIdToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioIdToken == null || usuarioIdToken != id.ToString())
                return Forbid();

            var resultado = await _usuarioService.DeletarUsuario(id);
            if (!resultado)
                return NotFound();

            return Ok(new { Mensagem = "Usuário deletado com sucesso." });
        }
    }
}
