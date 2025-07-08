using AspNetCoreGeneratedDocument;
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
    public class PostergacionesCursosController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;
        private string idUsuarioActivo = "0000000001";
        public PostergacionesCursosController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            //Lista de postergaciones (Tipo 4)

            var conceptoPostergacion = _appdbContext.tarifarioOtrosListas.Where(x => x.tipoConcepto == 4 && x.academico == 1).ToList();

            var listaSedes=_appdbContext.institucionSedes.Where(x=>x.Estado==1).ToList();

            var listaperiodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();

            ViewBag.listaSedes = listaSedes;
            ViewBag.listaPeriodos= listaperiodos;

            ViewBag.listaconceptoPostergacion= conceptoPostergacion;

            return View();
        }



        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena,int idSede, string periodo)
        {

            if (cadena is null)
            {
                cadena = "";
            }
            //var parametro = new SqlParameter("@cadenabuscar", cadena);

            var parametro = new[]
           {
               // new SqlParameter("@returnopcion",System.Data.SqlDbType.Int) { Direction=System.Data.ParameterDirection.Output},
                new SqlParameter("@cadenabuscar",cadena),
                new SqlParameter("@idSede",idSede),
                new SqlParameter("@periodo",periodo)
            };

            var resultadoBusqueda = _appdbContext.alumnoBusquedas.FromSqlRaw("CajaWeb.sp_busquedaAlumnosPostergados @cadenabuscar,@idSede,@periodo", parametro).ToList();
            return Json(resultadoBusqueda);
        }

        [HttpPost]
        public PartialViewResult TablaPostergados(string periodo, string sede)
        {

            var consulta = (from postergados in _appdbContext.postergacionesCursosListas.Where(x=>x.idSede==int.Parse(sede)&&x.periodo==periodo).ToList()
                            join persona in _appdbContext.personaPersona.ToList()
                            on postergados.idPersona equals persona.IdPersona
                            select new
                            {
                                postergados.idPostergacion,
                                postergados.idPersona,
                                postergados.descripcion,
                                postergados.estado,
                                postergados.codCurso,
                                persona.ApellidoPaterno,
                                persona.ApellidoMaterno,
                                persona.Nombres1,
                                persona.Nombres2,
                                persona.Email,
                                persona.Telefono

            }
            );

            ViewBag.Periodo = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();
            ViewBag.Sede = _appdbContext.institucionSedes.Where(x => x.IdSede == byte.Parse(sede)).SingleOrDefault();
            return PartialView(consulta);
        }


        [HttpPost]
        public JsonResult registrarPostergacionCurso(string idPersona,string concepto,string periodo,int sede)
        {

            idUsuarioActivo = HttpContext.Session.GetString("idPersona");

            var parameters = new[]
            {
                new SqlParameter("@idPersona",idPersona),
                new SqlParameter("@concepto",concepto),
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@sede",sede),
                new SqlParameter("@usuarioRegistro",idUsuarioActivo)
            };

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<AuxiliarString> resultado = new List<AuxiliarString>();
                    resultado = _appdbContext.auxiliarStrings.FromSqlRaw("CajaWeb.sp_registrarPostergacionCurso @idPersona,@concepto,@periodo,@sede,@usuarioRegistro", parameters).ToList();

                    scope.Complete();
                    return Json(resultado);
                }
            }
            catch (Exception e)
            {
                List<AuxiliarString> resultado = new List<AuxiliarString>();
                AuxiliarString res = new AuxiliarString();

                res.cadenaString = e.Message;
                resultado.Add(res);
                return Json(resultado);
            }

           
        }

        [HttpPost]

        public JsonResult eliminarPostergacion(int idPostergacion)
        {
            var parametros = new[]
            {
                new SqlParameter("@idPostergacion",idPostergacion),
            };

            try
            {

                _appdbContext.Database.ExecuteSqlRaw("exec CajaWeb.sp_eliminarPostergacionCurso @idPostergacion", parametros);
                return Json('1');

            }
            catch (Exception e)
            {
               
                return Json(e.Message);
            }


        }


        [HttpPost]
        public PartialViewResult modalDetallePago(int idPostergacion)
        {

            var parametros = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idTransaccionDetalle",idPostergacion),
            };

            var cabecera = (from cabeza in _appdbContext.ubicacionDetalleModal.FromSqlRaw("Intranet.sp_cajaDetallePostergacionCurso @opcion, @idTransaccionDetalle", parametros).ToList()
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
                    transaccionDetalleCabecera.cajero = item.cajero;

                }


            }



            return PartialView(transaccionDetalleCabecera);
        }




    }
}
