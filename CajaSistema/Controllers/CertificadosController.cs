using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class CertificadosController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;
        public CertificadosController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var sedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();
            var periodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();


            ViewBag.sedes=sedes;
            ViewBag.periodos=periodos;

            return View();
        }

        [HttpPost]
        public PartialViewResult TablaDetalle(string periodo,string idSede)
        {
            var parametro = new[]
            {
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@sede",idSede)
            };


            var listaCertificados = (from certificado in _appdbContext.certificadosListas.FromSqlRaw("[CajaWeb].[sp_listaCertificadosPeriodo] @periodo, @sede", parametro).ToList()
                               join per in _appdbContext.listaPeriodosPeriodos
                               on certificado.periodo equals per.periodoTexto
                               select new
                               {
                                   certificado.idCertificado,
                                   certificado.idPersona,
                                   certificado.nombres,
                                   certificado.numeroContacto,
                                   certificado.correo,
                                   certificado.monto,
                                   certificado.fechaRegistro,
                                   per.periodoDescripcion,
                                   certificado.certificados,
                                   certificado.estado
                               }
                             );

            ViewBag.Periodo = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();
            ViewBag.Sede = _appdbContext.institucionSedes.Where(x => x.IdSede == byte.Parse(idSede)).SingleOrDefault();


            return PartialView(listaCertificados);
        }

        [HttpPost]
        public PartialViewResult modalTransacciones(int idCertificado)
        {
            var parametros = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCertificado",idCertificado)
            };

            CajaTransaccionDetalleCabecera transaccionDetalleCabecera = new CajaTransaccionDetalleCabecera();

            var cabecera = (from cabeza in _appdbContext.cajaTransaccionDetalleCabeceras.FromSqlRaw("Intranet.sp_certificadoDetalle @opcion, @idCertificado", parametros).ToList()
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
                    new SqlParameter("@idCertificado",idCertificado),
                };

                transaccionDetalleCabecera.DetallePago = _appdbContext.cajaTransaccionDetalleCuerpos.FromSqlRaw("Intranet.sp_certificadoDetalle @opcion, @idCertificado", par).ToList();
            }


            return PartialView(transaccionDetalleCabecera);
        }

        [HttpPost]
        public PartialViewResult reporteNotas(int idCertificado)
        {

            var par = new[]
                {
                    new SqlParameter("@Opcion",1),
                    new SqlParameter("@IdCertificado",idCertificado),
                };

            var reporteNotas = _appdbContext.reporteNotasListas.FromSqlRaw("Intranet.sp_reporteNotas @Opcion,@IdCertificado", par).AsEnumerable().OrderByDescending(x=>x.Fecha).ToList();

            var par2 = new[]
            {
                new SqlParameter("@Opcion",2),
                new SqlParameter("@IdCertificado",idCertificado),
            };
            var cadena = _appdbContext.auxiliarStrings.FromSqlRaw("Intranet.sp_reporteNotas @Opcion,@IdCertificado", par2).ToList();

            ViewBag.auxiliarString = cadena;

            return PartialView(reporteNotas);
        }

        [HttpPost]
        public JsonResult eliminarCertificado(int idCertificado)
        {
            var parameters = new[]
            {
               // new SqlParameter("@returnopcion",System.Data.SqlDbType.Int) { Direction=System.Data.ParameterDirection.Output},
                new SqlParameter("@idCertificado",idCertificado),
            };
            try
            {
                _appdbContext.Database.ExecuteSqlRaw("exec Intranet.sp_eliminarConstanciaCertificado @idCertificado", parameters);
                return Json('1');
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }

    }
}
