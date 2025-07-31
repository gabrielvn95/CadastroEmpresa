using CadastroEmpresa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CadastroEmpresa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaInterface _empresaService;

        public EmpresaController(IEmpresaInterface empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] string cnpj)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var empresa = await _empresaService.CadastrarEmpresa(cnpj, usuarioId);
            return Ok(empresa);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var empresas = await _empresaService.ListarEmpresas(usuarioId);
            return Ok(empresas);
        }
    }
}
