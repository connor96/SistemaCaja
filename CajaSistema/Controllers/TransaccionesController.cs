using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;

        CajaTransaccionDetalleCabecera transaccionDetalleCabecera;

        private string idCajeroActivo = "1105240228";
        public TransaccionesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idCajeroActivo),
                new SqlParameter("@periodo","2501"),
                new SqlParameter("@sede",1)
            };

            var listaTransacciones = _appdbContext.cajaTransaccionCabecera.FromSqlRaw("CajaWeb.sp_CajaTransaccionCabecera @opcion,@idCajero,@periodo,@sede", parameters).ToList();

            return View(listaTransacciones);
        }

        [HttpPost]
        public JsonResult recargarTabla()
        {
            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idCajeroActivo),
                new SqlParameter("@periodo","2501"),
                new SqlParameter("@sede",1)
            };

            var listaTransacciones = _appdbContext.cajaTransaccionCabecera.FromSqlRaw("CajaWeb.sp_CajaTransaccionCabecera @opcion,@idCajero,@periodo,@sede", parameters).ToList();

            return Json(listaTransacciones);
        }

        [HttpPost]
        public PartialViewResult modalTransacciones(int id)
        {
            var parametros = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idTransaccionDetalle",id),
            };

            transaccionDetalleCabecera = new CajaTransaccionDetalleCabecera();

            var cabecera = (from cabeza in _appdbContext.cajaTransaccionDetalleCabeceras.FromSqlRaw("Intranet.sp_cajaTransaccionDetalle @opcion, @idTransaccionDetalle", parametros).ToList()
                            join banco in _appdbContext.bancoBancos.ToList()
                            on cabeza.CodigoBanco equals banco.codigoBanco
                            join canal in _appdbContext.bancoCanalesPagos.ToList()
                            on cabeza.CanalPago equals canal.codigoCanal
                            join forma in _appdbContext.bancoFormasPagos.ToList()
                            on cabeza.FormaPago equals forma.codigoFormaPago
                            join sede in _appdbContext.institucionSedes.ToList()
                            on cabeza.IdSede equals sede.IdSede
                            select new
                            {
                                cabeza.IdTransaccionesPagadas,
                                cabeza.IdPersona,
                                cabeza.Alumno,
                                cabeza.FechaTransaccion,
                                cabeza.HoraTransaccion,
                                banco.descripcionBanco,
                                canal.descripcionCanal,
                                forma.descripcionFormaPago,
                                cabeza.NumOperacionBanco,
                                cabeza.Cuenta,
                                cabeza.Monto,
                                sede.Sede
                            });
            if (cabecera.Count()>0)
            {
                foreach (var item in cabecera)
                {
                    transaccionDetalleCabecera.IdTransaccionesPagadas = item.IdTransaccionesPagadas;
                    transaccionDetalleCabecera.IdPersona = item.IdPersona;
                    transaccionDetalleCabecera.Alumno = item.Alumno;
                    transaccionDetalleCabecera.FechaTransaccion = item.FechaTransaccion;
                    transaccionDetalleCabecera.HoraTransaccion = item.HoraTransaccion;
                    transaccionDetalleCabecera.CodigoBanco = item.descripcionBanco;
                    transaccionDetalleCabecera.CanalPago = item.descripcionCanal;
                    transaccionDetalleCabecera.FormaPago = item.descripcionFormaPago;
                    transaccionDetalleCabecera.NumOperacionBanco = item.NumOperacionBanco;
                    transaccionDetalleCabecera.Cuenta = item.Cuenta;
                    transaccionDetalleCabecera.Monto = item.Monto;
                    transaccionDetalleCabecera.DetalleSede = item.Sede;

                }

                var par = new[]
                {
                    new SqlParameter("@opcion",2),
                    new SqlParameter("@idTransaccionDetalle",id),
                };

                transaccionDetalleCabecera.DetallePago=_appdbContext.cajaTransaccionDetalleCuerpos.FromSqlRaw("Intranet.sp_cajaTransaccionDetalle @opcion, @idTransaccionDetalle", par).ToList();
            }
            


            return PartialView(transaccionDetalleCabecera);
        }

        [HttpPost]
        public JsonResult actualizarPago(int idTransaccionPago)
        {
            var parametros = new[]
            {
                new SqlParameter("@idTransaccion",idTransaccionPago)
            };

            var command = "update Intranet.tb_TransaccionesPagadas set CajaEstado='1' where IdTransaccionesPagadas=@idTransaccion";

            try
            {
                _appdbContext.Database.ExecuteSqlRaw(command, parametros);
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
           
        }

    }
}
