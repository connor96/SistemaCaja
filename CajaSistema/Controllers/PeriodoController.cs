using AspNetCoreGeneratedDocument;
using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class PeriodoController : Controller
    {
        ListaPeriodo _periodoLista;

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


        [HttpPost]
        public JsonResult eliminarPeriodo(int idPeriodo)
        {
            _periodoLista = new ListaPeriodo { idPeriodoMatricula = idPeriodo };
            try
            {
                _appdbContext.listaPeriodosPeriodos.Attach(_periodoLista);
                _appdbContext.listaPeriodosPeriodos.Remove(_periodoLista);
                _appdbContext.SaveChanges();
                return Json("1");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }


        //public JsonResult agregarPeriodo()
        //{

        //}

    }
}
