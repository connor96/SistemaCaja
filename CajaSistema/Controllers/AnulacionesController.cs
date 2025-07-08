using CajaSistema.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,MARKETING,SECRESEDE")]
    public class AnulacionesController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;
        public AnulacionesController(ApplicationDbContext context)
        {
            
            _appdbContext = context;
            //_ = HttpContext.Session.GetString("idPersona");
            //if (idUsuarioActivo is null)
            //{
            //    Response.Redirect("./Login");
            //}

        }
        [HttpGet]
        public IActionResult Index()
        {
            //idUsuarioActivo= HttpContext.Session.GetString("idPersona");
            //if(idUsuarioActivo is null)
            //{
            //    return Redirect("/Authentication");
            //}
            return View();
        }

        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena)
        {
            //idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            //if (idUsuarioActivo is null)
            //{
            //    return Json("error");
            //}

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
            //try
            //{
            //    idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            //    if (idUsuarioActivo is null)
            //    {
            //        throw new Exception("error");
            //    }

                var parameter = new[]
                {
                    new SqlParameter("@idPersona",idPersona)
                };
                var listaPendientesAlumno = _appdbContext.listaEliminarAumnos.FromSqlRaw("CajaWeb.sp_listaPendientesAlumnoAnulaciones @idPersona", parameter).ToList();


                var datosAlumno = _appdbContext.personaPersona.Where(x => x.IdPersona == idPersona).SingleOrDefault();

                ViewBag.persona = datosAlumno;

                return PartialView(listaPendientesAlumno);

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
           

        }

        [HttpPost]
        public JsonResult EliminarTransaccion(int idTransaccion)
        {
            idUsuarioActivo = User.Identity?.Name;
            
            var parameter = new[]
            {
                new SqlParameter("@idTransaccion",idTransaccion)
            };

            var command = "exec [CajaWeb].[sp_eliminarTransaccion] @idTransaccion";
            _appdbContext.Database.ExecuteSqlRaw(command, parameter);


            return Json("ok");

        }
    }
}
