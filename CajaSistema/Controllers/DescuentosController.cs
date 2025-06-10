using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,MARKETING,SECRESEDE")]
    public class DescuentosController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;

        DescuentoDescuento _descuendoClase;


        public string idUsuarioActivo =  "0000000001";

        //List<DescuentoListaAlumnos> _listaAlumnosconDescuento;


        public DescuentosController(ApplicationDbContext context)
        {
            _appdbContext = context;

        }
        [HttpGet]
        public IActionResult Index()
        {


            var _listaDescuentosLista=_appdbContext.descuentoListaDescuentos.Where(s => s.Estado== 1).ToList();

            var listaDescuentos = _appdbContext.descuentoListas.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosDescuentos").ToList();

            //var listDescuentos = _appdbContext.descuentoDescuento.ToList();


            ViewBag.listaDescuento = _listaDescuentosLista;
            ViewBag.listaDescuentos = listaDescuentos;
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
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnosDescuento @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }


        [HttpPost]
        public  JsonResult RegistrarAlumnoDescuento(string idPersona, string idDescuento)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            _descuendoClase = new DescuentoDescuento();
            _descuendoClase.idDescuentoConcepto = byte.Parse(idDescuento);
            _descuendoClase.idPersona = idPersona;
            _descuendoClase.estado = 1;
            _descuendoClase.monto = 25;
            _descuendoClase.usuarioRegistro = idUsuarioActivo;
            _descuendoClase.fechaRegistro = DateTime.Now;

            int id = 0;
            try
            {
                _appdbContext.descuentoDescuento.Add(_descuendoClase);
                _appdbContext.SaveChanges();
                id = _descuendoClase.idDescuento;
                return Json(id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult EliminarAlumnoDescuento(string idDescuento)
        {
            _descuendoClase = new DescuentoDescuento { idDescuento = int.Parse(idDescuento) };


            try
            {
                _appdbContext.descuentoDescuento.Attach(_descuendoClase);
                _appdbContext.descuentoDescuento.Remove(_descuendoClase);
                _appdbContext.SaveChanges();
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult RetirarAlumnoDescuento(string idDescuento)
        {
            _descuendoClase = new DescuentoDescuento();
            try
            {
                if(idDescuento is null) { idDescuento = "0"; }

                _descuendoClase = _appdbContext.descuentoDescuento.Find(int.Parse(idDescuento));

                if (_descuendoClase.idDescuento == 0)
                {
                    return Json("Error");
                }
                else
                {
                    _descuendoClase.estado = 0;
                    _appdbContext.descuentoDescuento.Update(_descuendoClase);
                    _appdbContext.SaveChanges();
                    return Json("ok");
                }

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult ActivarAlumnoDescuento(string idDescuento)
        {
            _descuendoClase = new DescuentoDescuento();
            try
            {
                if (idDescuento is null) { idDescuento = "0"; }

                _descuendoClase = _appdbContext.descuentoDescuento.Find(int.Parse(idDescuento));

                if (_descuendoClase.idDescuento == 0)
                {
                    return Json("Error");
                }
                else
                {
                    _descuendoClase.estado = 1;
                    _appdbContext.descuentoDescuento.Update(_descuendoClase);
                    _appdbContext.SaveChanges();
                    return Json("ok");
                }

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public PartialViewResult tablaBusquedaAlumnos(string cadenaBusqueda, string opcion)
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
                var listaAlumnosBecados = _appdbContext.descuentoListas.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosDescuentos").ToList();

                var listaAlumnosBecados2 = listaAlumnosBecados.Where(s => s.nombresApellidos.Contains(cadenaBusqueda)).ToList();

                var listaAlumnosBecados3 = listaAlumnosBecados.Where(s => s.idPersona.Contains(cadenaBusqueda)).ToList();


                if (cadenaBusqueda is null || cadenaBusqueda == "")
                {

                }
                else if (listaAlumnosBecados2.Count < 1)
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
                var listaAlumnosDesaprobados = _appdbContext.descuentoListas.FromSqlRaw("EXEC CajaWeb.sp_listarAlumnosDescuentos").ToList();

                listaAlumnosDesaprobados = listaAlumnosDesaprobados.Where(s => s.Aprobado == false).ToList();


                ViewBag.listaAlumnos = listaAlumnosDesaprobados;
            }


            return PartialView();


        }




    }
}
