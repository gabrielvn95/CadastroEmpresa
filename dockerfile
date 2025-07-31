using Microsoft.AspNetCore.Mvc;

namespace CadastroEmpresa
{
    public class dockerfile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
