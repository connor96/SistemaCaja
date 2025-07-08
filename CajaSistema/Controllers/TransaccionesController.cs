using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CajaSistema.Controllers
{
    
    [Authorize(Roles = "ADMINISTRADOR,TESORERO,CAJERO,SECRESEDE")]
    
    public class TransaccionesController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;


        CajaTransaccionDetalleCabecera transaccionDetalleCabecera;


        private string idUsuarioActivo = "0000000001";
        public TransaccionesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");

            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idUsuarioActivo),
                new SqlParameter("@periodo","2502"),
                new SqlParameter("@sede",1)
            };

            var listaTransacciones = _appdbContext.cajaTransaccionCabecera.FromSqlRaw("CajaWeb.sp_CajaTransaccionCabecera @opcion,@idCajero,@periodo,@sede", parameters).ToList();

            return View(listaTransacciones);
        }

        [HttpPost]
        public JsonResult recargarTabla()
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idUsuarioActivo),
                new SqlParameter("@periodo","2502"),
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



            if (cabecera.Count()>0)
            {
                foreach (var item in cabecera)
                {
                    transaccionDetalleCabecera.IdTransaccionesPagadas = item.IdTransaccionesPagadas;
                    transaccionDetalleCabecera.IdPersona = item.IdPersona;
                    transaccionDetalleCabecera.DNI= item.DNI;
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
                    transaccionDetalleCabecera.Telefono=item.Telefono;
                    transaccionDetalleCabecera.exaUbicacion = item.exaUbicacion;

                }

                var par = new[]
                {
                    new SqlParameter("@opcion",2),
                    new SqlParameter("@idTransaccionDetalle",id),
                };

                transaccionDetalleCabecera.DetallePago=_appdbContext.cajaTransaccionDetalleCuerpos.FromSqlRaw("Intranet.sp_cajaTransaccionDetalle @opcion, @idTransaccionDetalle", par).ToList();
            }

            var p = new[]
            {
                new SqlParameter("@idPersona",transaccionDetalleCabecera.IdPersona)
            };



            return PartialView(transaccionDetalleCabecera);
        }


        [HttpPost]
        public PartialViewResult modalProcesados(int id)
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
        public JsonResult actualizarPago(int idTransaccionPago)
        {
            DateTime fecha = new DateTime();
            fecha = System.DateTime.Now;

            var parametros = new[]
            {
                new SqlParameter("@idTransaccion",idTransaccionPago),
            };

            var command = "exec CajaWeb.sp_procesarPago @idTransaccion";

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


        [HttpGet]
        public IActionResult ListaProcesados()
        {
            //var periodos = _appdbContext.listaPeriodosPeriodos.Where(x=>x.estado==true).ToList();
            var periodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x=>x.idPeriodoMatricula).ToList();

            return View(periodos);
        }

        [HttpPost]
        public PartialViewResult TablaProcesados(string periodo, string idCajero)
        {
            idCajero = HttpContext.Session.GetString("idPersona");

            var parametros = new[]
            {
                new SqlParameter("@idCajero",idCajero),
                new SqlParameter("@periodo",periodo)
            };

            var cajaTransaccionPagos=_appdbContext.cajaTransaccionPagados.FromSqlRaw("CajaWeb.sp_CajaPagosProcesados @idCajero, @periodo", parametros).ToList();

            var periodoElemento = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();

            ViewBag.periodoElemento = periodoElemento;

            return PartialView(cajaTransaccionPagos);
        }

        [HttpPost]
        public JsonResult observarPago(int idTransaccionPago,string observacion)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");

            var parametros = new[]
            {
                new SqlParameter("@idTransaccion",idTransaccionPago),
                new SqlParameter("@observacion",observacion),
                new SqlParameter("@idUsuarioActivo",idUsuarioActivo),
            };

            var command = "exec CajaWeb.sp_observarPago @idTransaccion,@observacion,@idUsuarioActivo";

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

        [HttpGet]
        public IActionResult ListaObservados()
        {
            //var periodos = _appdbContext.listaPeriodosPeriodos.Where(x=>x.estado==true).ToList();
            var periodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();

            return View(periodos);
        }
        [HttpPost]
        public PartialViewResult TablaObservados(string periodo, string idCajero)
        {
            idCajero = HttpContext.Session.GetString("idPersona");

            var parametros = new[]
            {
                new SqlParameter("@idCajero",idCajero),
                new SqlParameter("@periodo",periodo)
            };

            var cajaTransaccionPagos = _appdbContext.cajaTransaccionPagados.FromSqlRaw("CajaWeb.sp_CajaPagosObservados @idCajero,@periodo", parametros).ToList();

            var periodoElemento = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();

            ViewBag.periodoElemento = periodoElemento;

            return PartialView(cajaTransaccionPagos);
        }


        [HttpPost]
        public PartialViewResult modalObservados(int id)
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
                                cabeza.exaUbicacion,
                                cabeza.observacion
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
                    transaccionDetalleCabecera.observacion = item.observacion;

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



    }
}
