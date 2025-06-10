using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class TarifarioAcademicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
