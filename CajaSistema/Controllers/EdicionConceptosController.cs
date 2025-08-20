using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,TESORERO,CAJERO,SECRESEDE")]
    public class EdicionConceptosController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;
        public EdicionConceptosController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BusquedaAlumno(string texto)
        {

            var parameters = new[]
            {
                new SqlParameter("@opcion","BuscarCla"),
                new SqlParameter("@idperiodo",""),
                new SqlParameter("@codNivelCurso",""),
                new SqlParameter("@cadenabuscar",texto)
            };

            var listaTransacciones = _appdbContext.usuarioPasswords.FromSqlRaw("NetCore.sp_PromocionEdad_Listas @opcion,@idperiodo,@codNivelCurso,@cadenabuscar", parameters).ToList();

            return View(listaTransacciones);
        }

        [HttpPost]
        public PartialViewResult BusquedaTransacciones(string idPersona)
        {
            var persona = _appdbContext.personaPersona.Where(x => x.IdPersona == idPersona).SingleOrDefault();
            
            var parameters = new[]
           {
                new SqlParameter("@idPersona",idPersona)
            };


            var transacciones = _appdbContext.editarTransaccionesLista.FromSqlRaw("[CajaWeb].[sp_listaTransaccionesAlumno] @idPersona", parameters).ToList();


            ViewBag.persona = persona;


            //var transacciones=_appdbContext.
            return PartialView(transacciones);
        }

        [HttpPost]
        public PartialViewResult EdicionTransacciones(int idTransaccion)
        {

            var transaccionPrincipal=_appdbContext.intranetTransaccionesLista.Where(x=>x.IdTransacciones== idTransaccion).SingleOrDefault();
            var detalleTransaccion=_appdbContext.IntranetTransaccionesDetalleLista.Where(x=>x.IdTransacciones==idTransaccion).ToList();

            ViewBag.transaccion = transaccionPrincipal;
            ViewBag.detalleTransaccion=detalleTransaccion;

            return PartialView();
        }

        [HttpDelete]
        public JsonResult EliminarTransaccion(int idTransaccion,int idDetalleTransaccion)
        {
            respuestaConsulta respuesta= new respuestaConsulta();

            var detalleTransaccion = _appdbContext.IntranetTransaccionesDetalleLista.Where(x => x.IdTransacciones == idTransaccion).ToList();
            if (detalleTransaccion.Count < 2)
            {
                respuesta.idConsulta = 0;
                respuesta.mensaje = "No se puede eliminar";
                return Json(respuesta);
            }
            else
            {

                try
                {

                    using (var transaction = new TransactionScope())
                    {
                        var detalleEliminar = _appdbContext.IntranetTransaccionesDetalleLista
                                                .SingleOrDefault(x => x.IdTransaccionesDetalle == idDetalleTransaccion);

                        if (detalleEliminar != null)
                        {
                            _appdbContext.IntranetTransaccionesDetalleLista.Remove(detalleEliminar);
                            _appdbContext.SaveChanges();
                        }

                        decimal suma = _appdbContext.IntranetTransaccionesDetalleLista.Where(x => x.IdTransacciones == idTransaccion).Sum(x => x.Monto).Value;

                        IntranetTransacciones transaccion = new IntranetTransacciones();
                        transaccion = _appdbContext.intranetTransaccionesLista.Where(x => x.IdTransacciones == idTransaccion).SingleOrDefault();

                        transaccion.MontoTotal = suma;

                        _appdbContext.intranetTransaccionesLista.Update(transaccion);
                        _appdbContext.SaveChanges();

                        respuesta.idConsulta = 1;
                        respuesta.mensaje = "Se ha eliminado correctamente";

                        transaction.Complete();

                        return Json(respuesta);

                    }

                }
                catch (Exception e)
                {
                    respuesta.idConsulta = 3;
                    respuesta.mensaje = e.Message;


                    return Json(respuesta);
                }


            }


        }

       


    }

    public class respuestaConsulta
    {
        public int idConsulta { get; set; }
        public string? mensaje { get; set; }
    }
}
