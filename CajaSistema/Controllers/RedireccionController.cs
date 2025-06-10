using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Transactions;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,TESORERO")]
    public class RedireccionController : Controller
    {
        CajaTransaccionDetalleCabecera transaccionDetalleCabecera;

        private readonly ApplicationDbContext _appdbContext;
        public RedireccionController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var listaPeriodos=_appdbContext.listaPeriodosPeriodos.Where(x=>x.estado==true).ToList();
            ViewBag.listaPeriodos = listaPeriodos;

            var listaCajeros = (from cajeros in _appdbContext.cajeroCajeroActivos.ToList()
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

            var listaSedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();
            ViewBag.listaSedes= listaSedes;
            ViewBag.listaCajeros = listaCajeros;



            return View();
        }


        [HttpPost]
        public JsonResult CajerosActivosPeriodo(string periodo,string sede)
        {
            var parameter = new[]
            {
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@sede",sede)
            };

            var listaCajerosActivos = _appdbContext.redireccionCajeros.FromSqlRaw("CajaWeb.sp_listaCajerosActivosSede @periodo,@sede", parameter).ToList();
            
            return Json(listaCajerosActivos);
        }


        [HttpPost]
        public PartialViewResult TablaTransacciones(string idCajero)
        {

            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idCajero),
                new SqlParameter("@periodo","2502"),
                new SqlParameter("@sede",1)
            };

            var listaTransacciones = _appdbContext.cajaTransaccionListaPendientes.FromSqlRaw("CajaWeb.sp_CajaTransaccionListaCaja @opcion,@idCajero,@periodo,@sede", parameters).ToList();

            return PartialView(listaTransacciones);
        }

        [HttpPost]
        public PartialViewResult TablaTransaccionesTodos(string sede, string periodo)
        {

            var parameters = new[]
            {
                new SqlParameter("@sede",sede),
                new SqlParameter("@periodo",periodo),
            };

            var listaTransacciones = _appdbContext.cajaTransaccionListaTodos.FromSqlRaw("CajaWeb.sp_CajaTransaccionListaTodos @sede,@periodo", parameters).ToList();

            return PartialView(listaTransacciones);
        }

        [HttpPost]
        public JsonResult TransferirElementos(List<string> itemsTransferir,string idCajeroNuevo)
        {
            try
            {
                using (TransactionScope scope=new TransactionScope())
                {
                    foreach (var item in itemsTransferir)
                    {

                        var parametros = new[]
                        {
                            new SqlParameter("@itemTransferir",item),
                            new SqlParameter("@cajeroNuevo",idCajeroNuevo)
                        };

                        var command = "update Intranet.tb_TransaccionesPagadas set CajaUsuarioCajero=@cajeroNuevo where IdTransaccionesPagadas=@itemTransferir";
                        _appdbContext.Database.ExecuteSqlRaw(command, parametros);

                    }
                    scope.Complete();
                    return Json("ok");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            
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
                                cabeza.DNI,
                                cabeza.Alumno,
                                cabeza.FechaTransaccion,
                                cabeza.HoraTransaccion,
                                banco.descripcionBanco,
                                canal.descripcionCanal,
                                forma.descripcionFormaPago,
                                cabeza.NumOperacionBanco,
                                cabeza.Cuenta,
                                cabeza.Monto,
                                sede.Sede,
                                cabeza.Email,
                                cabeza.Telefono,
                                cabeza.exaUbicacion
                            });



            if (cabecera.Count() > 0)
            {
                foreach (var item in cabecera)
                {
                    transaccionDetalleCabecera.IdTransaccionesPagadas = item.IdTransaccionesPagadas;
                    transaccionDetalleCabecera.IdPersona = item.IdPersona;
                    transaccionDetalleCabecera.DNI = item.DNI;
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
                    transaccionDetalleCabecera.Email = item.Email;
                    transaccionDetalleCabecera.Telefono = item.Telefono;
                    transaccionDetalleCabecera.exaUbicacion = item.exaUbicacion;

                }

                var par = new[]
                {
                    new SqlParameter("@opcion",2),
                    new SqlParameter("@idTransaccionDetalle",id),
                };

                transaccionDetalleCabecera.DetallePago = _appdbContext.cajaTransaccionDetalleCuerpos.FromSqlRaw("Intranet.sp_cajaTransaccionDetalle @opcion, @idTransaccionDetalle", par).ToList();
            }

            var p = new[]
            {
                new SqlParameter("@idPersona",transaccionDetalleCabecera.IdPersona)
            };



            return PartialView(transaccionDetalleCabecera);
        }

        [HttpPost]
        public PartialViewResult modalResumenCajeros(string periodo, string sede)
        {

            var par = new[]
             {
                    new SqlParameter("@periodo",periodo),
                    new SqlParameter("@sede",sede),
             };
            var listaCajeros=_appdbContext.resumenCajerosPagosCaja.FromSqlRaw("CajaWeb.sp_resumenPagosCajero @periodo, @sede", par).ToList();

            var containerSede = _appdbContext.institucionSedes.Where(x => x.IdSede == int.Parse(sede)).SingleOrDefault();

            ViewBag.sede = containerSede;

            return PartialView(listaCajeros);
        }


    }
}
