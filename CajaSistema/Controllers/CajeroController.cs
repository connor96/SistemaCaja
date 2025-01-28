using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{
    public class CajeroController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        CajeroCajeroActivo _cajeroActivo;
        private readonly ApplicationDbContext _appdbContext;
        public CajeroController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var _listaCajerosActivos = (from cajeros in _appdbContext.cajeroCajeroActivos.ToList()
                                        join persona in _appdbContext.personaPersona.ToList()
                                        on cajeros.usuarioCajero equals persona.IdPersona
                                        select new
                                        {
                                            cajeros.idCajeroActivo,
                                            cajeros.usuarioCajero,
                                            persona.ApellidoPaterno,
                                            persona.ApellidoMaterno,
                                            persona.Nombres1,
                                            persona.Nombres2
                                        });
                
            ViewBag.listaCajerosActivos= _listaCajerosActivos;

            return View();
        }

        [HttpPost]
        public JsonResult BusquedaPersonaCajero(string cadena)
        {

            if (cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@cadenabuscar", cadena);
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaCajeros @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }

        [HttpPost]
        public JsonResult AgregarCajero(string idPersona)
        {
            _cajeroActivo = new CajeroCajeroActivo();
            _cajeroActivo.usuarioCajero= idPersona;
            _cajeroActivo.usuarioRegistro = idUsuarioActivo;
            _cajeroActivo.fechaRegistro = DateTime.Now;

            int id = 0;

            using (var dbContextTransacion = _appdbContext.Database.BeginTransaction())
            {
                try
                {
                    _appdbContext.cajeroCajeroActivos.Add(_cajeroActivo);
                    _appdbContext.SaveChanges();
                    id = _cajeroActivo.idCajeroActivo;
                    dbContextTransacion.Commit();
                    return Json(id);
                }
                catch (Exception e)
                {
                    dbContextTransacion.Rollback();
                    return Json(e.Message);
                }
            }


        }

        [HttpPost]
        public JsonResult EliminarCajero(string idCajero)
        {
            _cajeroActivo = new CajeroCajeroActivo { idCajeroActivo = int.Parse(idCajero) };

            using (var transaction=_appdbContext.Database.BeginTransaction())
            {
                try
                {
                    _appdbContext.cajeroCajeroActivos.Attach(_cajeroActivo);
                    _appdbContext.cajeroCajeroActivos.Remove(_cajeroActivo);
                    _appdbContext.SaveChanges();
                    transaction.Commit();
                    return Json("ok");
                }
                catch (Exception e)
                {
                    transaction.Rollback(); 
                    return Json(e.Message);

                }
            }

        }

        [HttpGet]
        public IActionResult AsignarCajero()
        {
            var _listaSedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();
            var _listaPeriodo = _appdbContext.periodoIntranets.ToList();
            
            ViewBag.periodo=_listaPeriodo;
            return View(_listaSedes);
        }

        [HttpPost]
        public JsonResult _listaCajeros()
        {
            var _listaCajeros = (from cajero in _appdbContext.cajeroCajeroActivos.ToList()
                                 join persona in _appdbContext.personaPersona.ToList()
                                 on cajero.usuarioCajero equals persona.IdPersona
                                 select new
                                 {
                                     cajero.idCajeroActivo,
                                     cajero.usuarioCajero,
                                     persona.ApellidoPaterno,
                                     persona.ApellidoMaterno,
                                     persona.Nombres1
                                 }).Except();

            //var _listaCajeros = (from cajero in _appdbContext.cajeroCajeroActivos.ToList()
            //                     join persona in _appdbContext.personaPersona.ToList()
            //                     on cajero.usuarioCajero equals persona.IdPersona
            //                     select new
            //                     {
            //                         cajero.idCajeroActivo,
            //                         cajero.usuarioCajero,
            //                         persona.ApellidoPaterno,
            //                         persona.ApellidoMaterno,
            //                         persona.Nombres1
            //                     });

            return Json(_listaCajeros);
        }

        [HttpPost]
        public JsonResult RegistrarCajero(CajeroAsignacionCajero _cajeroRegistro)
        {
            _cajeroRegistro.estado = true;
            _cajeroRegistro.fechaRegistro = DateTime.Now;
            _cajeroRegistro.usuarioRegistro=idUsuarioActivo;

            int id = 0;

            using (var transaction =_appdbContext.Database.BeginTransaction())
            {
                try
                {
                    _appdbContext.cajeroAsignacionCajeros.Add(_cajeroRegistro);
                    _appdbContext.SaveChanges();
                    id = _cajeroRegistro.idCajeroAsignacion;
                    transaction.Commit();
                    return Json(id);

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return Json(e.Message);
                }
            }

        }

    }
}
