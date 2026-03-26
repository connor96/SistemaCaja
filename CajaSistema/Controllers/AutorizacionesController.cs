using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    public class AutorizacionesController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;

        public AutorizacionesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var periodoActivo = _appdbContext.listaPeriodosPeriodos.Where(x => x.estado == true).SingleOrDefault();

            var niveles = await _appdbContext.programacionNivels.Where(x => x.EsCurso == true).OrderBy(x => x.Orden).ToListAsync();

            ViewBag.periodoActivo = periodoActivo;
            ViewBag.niveles = niveles;

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> _obtenerCursos(string codNivel)
        {
            var cursos = await _appdbContext.programacionNivels
                .Where(x => x.CodNivelCurso == codNivel && x.EsCurso == true)
                .OrderBy(x => x.Orden)
                .Select(x => new
                {
                    value = x.CodNivelCurso,
                    text = x.DesNivelCurso
                })
                .ToListAsync();

            return Json(cursos);
        }

    }
}
