using CajaSistema.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,TESORERO,CAJERO,MARKETING,SECRESEDE")]
    public class PendientesController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;
        public PendientesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena)
        {

            if (cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@cadenabuscar", cadena);
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnosPendientes @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }

        [HttpPost]
        public PartialViewResult ListaPendientes(string idPersona)
        {

            var parameter = new[]
            {
                new SqlParameter("@idPersona",idPersona)
            };
            var listaPendientesAlumno = _appdbContext.listaPendientesAlumnos.FromSqlRaw("CajaWeb.sp_listaPendientesAlumno @idPersona", parameter).ToList();


            var datosAlumno=_appdbContext.personaPersona.Where(x=>x.IdPersona==idPersona).SingleOrDefault();

            ViewBag.persona = datosAlumno;

            return PartialView(listaPendientesAlumno);

        }

    }
}
