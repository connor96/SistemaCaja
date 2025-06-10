using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,SECRESEDE")]
    public class OtrosConceptosController : Controller
    {

        private readonly ApplicationDbContext _appdbContext;
        private string idUsuarioActivo = "0000000001";

        public OtrosConceptosController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            List<AuxDobleString> doble= new List<AuxDobleString>();

            doble = agregarCategorias();

            var listaSedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();

            var listaperiodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();

            ViewBag.listaSedes = listaSedes;
            ViewBag.listaPeriodos = listaperiodos;

            ViewBag.listaConceptos = doble;

            return View();

        }

        private List<AuxDobleString> agregarCategorias()
        {
            
            List<AuxDobleString> listaDoble= new List<AuxDobleString>();
            
            AuxDobleString double1 = new AuxDobleString();
            double1.cadena1 = "1";
            double1.cadena2 = "CAMBIO DE HORARIO";
            listaDoble.Add(double1);

            AuxDobleString double2 = new AuxDobleString();
            double2.cadena1 = "3";
            double2.cadena2 = "POSTERGACION DE EXAMENES";
            listaDoble.Add(double2);

            AuxDobleString double3 = new AuxDobleString();
            double3.cadena1 = "5";
            double3.cadena2 = "GRADUACIONES";
            listaDoble.Add(double3);

            AuxDobleString double4 = new AuxDobleString();
            double4.cadena1 = "6";
            double4.cadena2 = "EXAMENES";
            listaDoble.Add(double4);

            return listaDoble;
        }

        [HttpPost]
        public PartialViewResult TablaConceptos(string periodo, string sede,int concepto)
        {

            var listaConceptos = (from conceptos in _appdbContext.otrosConceptosVarios.Where(x => x.periodo == periodo && x.idSede == int.Parse(sede)).ToList()
                                   join persona in _appdbContext.personaPersona.ToList()
                                   on conceptos.idPersona equals persona.IdPersona
                                   join per in _appdbContext.listaPeriodosPeriodos
                                   on conceptos.periodo equals per.periodoTexto
                                   join tarifario in _appdbContext.tarifarioOtrosListas.Where(x=>x.tipoConcepto==concepto).ToList()
                                   on conceptos.idTarifarioOtros equals tarifario.idTarifario
                                   select new
                                   {
                                       conceptos.idConceptosVarios,
                                       conceptos.idPersona,
                                       persona.ApellidoPaterno,
                                       persona.ApellidoMaterno,
                                       persona.Nombres1,
                                       persona.Nombres2,
                                       persona.Email,
                                       persona.Telefono,
                                       conceptos.codCurso,
                                       conceptos.estado,
                                       conceptos.descripcion,
                                   }
                                 );


            List<AuxDobleString> categorias= new List<AuxDobleString>();
            categorias = agregarCategorias();

            ViewBag.concepto = categorias.Where(x => x.cadena1 == concepto.ToString()).SingleOrDefault();
            ViewBag.Periodo = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();
            ViewBag.Sede = _appdbContext.institucionSedes.Where(x => x.IdSede == byte.Parse(sede)).SingleOrDefault();
            

            return PartialView(listaConceptos);
        }

        [HttpPost]
        public JsonResult ListaConceptos(int tipoConcepto)
        {
            var conceptosLista = _appdbContext.tarifarioOtrosListas.Where(_ => _.tipoConcepto == tipoConcepto).ToList();

            return Json(conceptosLista);
        }

        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena, int idSede, string periodo,string concepto)
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
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@concepto",concepto)
            };

            var resultadoBusqueda = _appdbContext.alumnoBusquedas.FromSqlRaw("CajaWeb.sp_busquedaAlumnosOtrosConceptos @cadenabuscar,@idSede,@periodo,@concepto", parametro).ToList();
            return Json(resultadoBusqueda);
        }

        [HttpPost]
        public JsonResult registrarConcepto(string idPersona, string concepto, string periodo, int sede)
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
                    resultado = _appdbContext.auxiliarStrings.FromSqlRaw("CajaWeb.sp_registrarOtrosConceptos @idPersona,@concepto,@periodo,@sede,@usuarioRegistro", parameters).ToList();

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
        public JsonResult eliminarConcepto(string idConcepto)
        {
            var parametros = new[]
            {
                new SqlParameter("@idConcepto",idConcepto),
            };

            try
            {

                _appdbContext.Database.ExecuteSqlRaw("exec CajaWeb.sp_eliminarOtrosConceptos @idConcepto", parametros);
                return Json('1');

            }
            catch (Exception e)
            {

                return Json(e.Message);
            }


        }

        [HttpPost]
        public PartialViewResult modalDetallePago(int idConcepto)
        {

            var parametros = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idConcepto",idConcepto),
            };

            var cabecera = (from cabeza in _appdbContext.ubicacionDetalleModal.FromSqlRaw("Intranet.sp_cajaDetalleOtrosProductos @opcion, @idConcepto", parametros).ToList()
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
