using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    public class ListaPendientesController : Controller
    {

        public string idUsuarioActivo = "0000000001";

        private readonly ApplicationDbContext _appdbContext;
        public ListaPendientesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
