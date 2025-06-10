using CajaSistema.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class PeriodoController : Controller
    {

        private readonly ApplicationDbContext _appdbContext;
        //private string idUsuarioActivo = "0000000001";
        public PeriodoController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var periodoLista = _appdbContext.listaPeriodosPeriodos.ToList();

            return View(periodoLista);
        }

        //public JsonResult agregarPeriodo()
        //{

        //}

    }
}
