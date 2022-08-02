using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
    public class AgendaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
    }
}
