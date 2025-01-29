using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    public class TransaccionesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
