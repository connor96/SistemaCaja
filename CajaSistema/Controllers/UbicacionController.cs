using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class UbicacionController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;

        private string idUsuarioActivo = "0000000001";

        public UbicacionListaExamen ubicacionAlumno;

        public UbicacionController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var sedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();
            var periodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();

            ViewBag.Sedes = sedes;
            return View(periodos);
        }

        [HttpPost]
        public PartialViewResult TablaUbicacion(string periodo, string idSede)
        {
            var listaExamen = (from examen in _appdbContext.ubicacionListaExamenes.Where(x => x.Periodo == periodo && x.IdSede == idSede).ToList()
                               join persona in _appdbContext.personaPersona.ToList()
                               on examen.IdPersona equals persona.IdPersona
                               join per in _appdbContext.listaPeriodosPeriodos
                               on examen.Periodo equals per.periodoTexto
                               select new
                               {
                                   examen.IdExamenUbicacion,
                                   examen.IdPersona,
                                   persona.ApellidoPaterno,
                                   persona.ApellidoMaterno,
                                   persona.Nombres1,
                                   persona.Nombres2,
                                   persona.Email,
                                   persona.Telefono,
                                   examen.CodCurso,
                                   examen.Estado,
                               }
                             );

            ViewBag.Periodo = _appdbContext.listaPeriodosPeriodos.Where(x=>x.periodoTexto==periodo).SingleOrDefault();
            ViewBag.Sede = _appdbContext.institucionSedes.Where(x => x.IdSede == byte.Parse(idSede)).SingleOrDefault();
            return PartialView(listaExamen);
        }

        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena)
        {
            if (cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@cadenabuscar", cadena);
            var listaAlumnos = _appdbContext.ubicacionAlumnoBusquedas.FromSqlRaw("CajaWeb.sp_busquedaAlumnosUbicacion @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }

        [HttpPost]
        public JsonResult registrarUbicacion(string idPersona,string idSede, string periodo)
        {
            idUsuarioActivo = HttpContext.Session.GetString("idPersona");
            ubicacionAlumno = new UbicacionListaExamen();
            ubicacionAlumno.IdPersona = idPersona;
            ubicacionAlumno.CodCurso = "";
            ubicacionAlumno.Estado = 1;
            ubicacionAlumno.Periodo= periodo;
            ubicacionAlumno.IdSede = idSede;
            ubicacionAlumno.UsuarioRegistro = idUsuarioActivo;
            ubicacionAlumno.FechaRegistro = DateTime.Now;
            var parameters = new[]
           {
               // new SqlParameter("@returnopcion",System.Data.SqlDbType.Int) { Direction=System.Data.ParameterDirection.Output},
                new SqlParameter("@opcion",1),
                new SqlParameter("@idPersona",idPersona),
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@sede",idSede),
                new SqlParameter("@usuarioRegistro",idUsuarioActivo)
            };
            int id = 0;
            try
            {
                using (TransactionScope scope=new TransactionScope())
                {
                    List<AuxiliarString> resultado = new List<AuxiliarString>();
                    resultado = _appdbContext.auxiliarStrings.FromSqlRaw("CajaWeb.sp_registrarExamenUbicacion @opcion,@idPersona,@periodo,@sede,@usuarioRegistro", parameters).ToList();
                    ubicacionAlumno.IdTransaccion = int.Parse(resultado[0].cadenaString);
                    _appdbContext.ubicacionListaExamenes.Add(ubicacionAlumno);
                    _appdbContext.SaveChanges();
                    id = ubicacionAlumno.IdExamenUbicacion;
                    scope.Complete();
                    return Json(id);
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult eliminarUbicacion(int idExamenUbicacion)
        {
            var parameters = new[]
            {
               // new SqlParameter("@returnopcion",System.Data.SqlDbType.Int) { Direction=System.Data.ParameterDirection.Output},
                new SqlParameter("@idExamenUbicacion",idExamenUbicacion),
            };
            try
            {
                _appdbContext.Database.ExecuteSqlRaw("exec CajaWeb.sp_eliminarExamenUbicacion @idExamenUbicacion", parameters);
                return Json('1');
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public PartialViewResult modalDetallePago(int idExamenUbicacion)
        {

            var parametros = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idTransaccionDetalle",idExamenUbicacion),
            };

            var cabecera = (from cabeza in _appdbContext.ubicacionDetalleModal.FromSqlRaw("Intranet.sp_cajaDetalleUbicacion @opcion, @idTransaccionDetalle", parametros).ToList()
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
                                cabeza.Descripcion,
                                cabeza.cajero
                            });

            UbicacionDetalleModal transaccionDetalleCabecera = new UbicacionDetalleModal();

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
                    transaccionDetalleCabecera.Descripcion = item.Descripcion;
                    transaccionDetalleCabecera.cajero= item.cajero;

                }

               
            }



            return PartialView(transaccionDetalleCabecera);
        }


    }
}
