using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class BecadosController : Controller
    {

        public string idUsuarioActivo = "0000000001";

        public BecadosAlumnoBecado _becadoAlumnoBecado;
        

        private readonly ApplicationDbContext _appdbContext;
        public BecadosController(ApplicationDbContext context)
        {
            _appdbContext = context;
            
        }



        [HttpGet]
        public IActionResult Index()
        {
            var listaAlumnosBecados = _appdbContext.becadosListaAlumnos.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosBecados").ToList();
            ViewBag.listaAlumnos = listaAlumnosBecados;
            return View();
        }

        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena)
        {

            if(cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@cadenabuscar", cadena);
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnosDescuento @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }


        [HttpPost]
        public JsonResult BusquedaAlumnosIdPersona(string cadena)
        {

            if (cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@idPersona", cadena);
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnosIdPersona @idPersona", parametro).ToList();
            return Json(listaAlumnos);
        }

        //[HttpPost]
        //public JsonResult MostrarAlumnoSeleccionado(string idPersona)
        //{
        //    var parametro = new SqlParameter("@cadenabuscar", idPersona);
        //    var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnos @cadenabuscar",parametro).ToList();
        //    return Json(listaAlumnos);
        //}


        [HttpPost]
        public JsonResult RegistrarBecado(string idPersona)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            _becadoAlumnoBecado = new BecadosAlumnoBecado();
            _becadoAlumnoBecado.idPersona = idPersona;
            _becadoAlumnoBecado.estado = true;
            _becadoAlumnoBecado.usuarioRegistro = idUsuarioActivo;
            _becadoAlumnoBecado.fechaRegistro = DateTime.Now;
            int id = 0;
            try
            {
                _appdbContext.becadosAlumnoBecados.Add(_becadoAlumnoBecado);
                _appdbContext.SaveChanges();
                id = _becadoAlumnoBecado.idBecado;
                return Json(id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }


        [HttpPost]
        public JsonResult EliminarBecado(string idBecado)
        {
            _becadoAlumnoBecado = new BecadosAlumnoBecado { idBecado=int.Parse(idBecado) };
            

            try
            {
                _appdbContext.becadosAlumnoBecados.Attach(_becadoAlumnoBecado);
                _appdbContext.becadosAlumnoBecados.Remove(_becadoAlumnoBecado);
                _appdbContext.SaveChanges();
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        [HttpPost]
        public JsonResult ActivarBecado(string idBecado)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            _becadoAlumnoBecado = new BecadosAlumnoBecado();
            _becadoAlumnoBecado = _appdbContext.becadosAlumnoBecados.SingleOrDefault(x => x.idBecado == int.Parse(idBecado));
            _becadoAlumnoBecado.estado = true;
            _becadoAlumnoBecado.usuarioRegistro = idUsuarioActivo;

            try
            {
                _appdbContext.becadosAlumnoBecados.Update(_becadoAlumnoBecado);
                _appdbContext.SaveChanges();
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        [HttpPost]
        public JsonResult DesactivarBecado(string idBecado)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            _becadoAlumnoBecado = new BecadosAlumnoBecado();
            _becadoAlumnoBecado = _appdbContext.becadosAlumnoBecados.SingleOrDefault(x => x.idBecado == int.Parse(idBecado));
            _becadoAlumnoBecado.estado = false;
            _becadoAlumnoBecado.usuarioRegistro = idUsuarioActivo;

            try
            {
                _appdbContext.becadosAlumnoBecados.Update(_becadoAlumnoBecado);
                _appdbContext.SaveChanges();
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        [HttpPost]
        public PartialViewResult tablaBusquedaAlumnos(string cadenaBusqueda,string opcion)
        {
            if (cadenaBusqueda is null)
            {
                cadenaBusqueda = "";
                cadenaBusqueda = cadenaBusqueda.ToUpper();
            }
            else
            {
                cadenaBusqueda = cadenaBusqueda.ToUpper();
            }
            

            if (opcion == "1")
            {
                var listaAlumnosBecados = _appdbContext.becadosListaAlumnos.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosBecados").ToList();

                var listaAlumnosBecados2 = listaAlumnosBecados.Where(s => s.nombresApellidos.Contains(cadenaBusqueda)).ToList();

                var listaAlumnosBecados3= listaAlumnosBecados.Where(s => s.idPersona.Contains(cadenaBusqueda)).ToList();


                if(cadenaBusqueda is null || cadenaBusqueda=="")
                {
                    
                }
                else if(listaAlumnosBecados2.Count<1)
                {
                    listaAlumnosBecados = listaAlumnosBecados3;

                }
                else if (listaAlumnosBecados3.Count < 1)
                {
                    listaAlumnosBecados = listaAlumnosBecados2;
                }
                else
                {
                    listaAlumnosBecados = listaAlumnosBecados2;

                    foreach (var item in listaAlumnosBecados3)
                    {
                        listaAlumnosBecados.Add(item);
                    }
                }
                

                ViewBag.listaAlumnos = listaAlumnosBecados;


            }
            else
            {
                var listaAlumnosDesaprobados = _appdbContext.becadosListaAlumnos.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosBecados").ToList();

                listaAlumnosDesaprobados = listaAlumnosDesaprobados.Where(s => s.Aprobado == false).ToList();


                ViewBag.listaAlumnos = listaAlumnosDesaprobados;
            }


            return PartialView();

        }



    }
}
