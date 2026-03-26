using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    public class TrasladoAcademicoController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;

        public TrasladoAcademicoController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaPeriodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x=>x.idPeriodoMatricula).ToList();

            return View(listaPeriodos);
        }
    }
}
