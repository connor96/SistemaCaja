using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,TESORERO,CAJERO,SECRESEDE")]
    public class ConceptosLibresController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;
        private string idUsuarioActivo = "0000000001";
        public ConceptosLibresController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var periodos = _appdbContext.listaPeriodosPeriodos.OrderByDescending(x => x.idPeriodoMatricula).ToList();

            var listaSedes = _appdbContext.institucionSedes.Where(x => x.Estado == 1).ToList();
            ViewBag.listaSedes = listaSedes;

            ViewBag.periodo = periodos;
            return View();
        }

        [HttpPost]
        public PartialViewResult TablaConceptos(string periodo)
        {

            var parametro = new[]
            {
                new SqlParameter("@periodo",periodo)
            };

            

            var listaConceptosLibres = _appdbContext.tablaConceptosLibre.FromSqlRaw("[CajaWeb].[sp_listaConceptosPeriodo] @periodo", parametro).ToList();
                                    

            ViewBag.Periodo = _appdbContext.listaPeriodosPeriodos.Where(x => x.periodoTexto == periodo).SingleOrDefault();



            return PartialView(listaConceptosLibres);
        }


        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena)
        {

            if (cadena is null)
            {
                cadena = "";
            }
            var parametro = new SqlParameter("@cadenabuscar", cadena);
            var listaAlumnos = _appdbContext.becadosListaALumnosBusqueda.FromSqlRaw("CajaWeb.sp_busquedaAlumnosPendientes @cadenabuscar", parametro).ToList();
            return Json(listaAlumnos);
        }

        [HttpPost]
        public JsonResult CargarAlumno(string idPersona)
        {
            var alumno=_appdbContext.personaPersona.Where(x=>x.IdPersona==idPersona).SingleOrDefault();

            return Json(alumno);
        }



        [HttpPost]
        public JsonResult registrarConcepto(string idPersona, string concepto, decimal monto, string periodo, int sede)
        {

            idUsuarioActivo = HttpContext.Session.GetString("idPersona");

            var parameters = new[]
            {
                new SqlParameter("@idPersona",idPersona),
                new SqlParameter("@concepto",concepto),
                new SqlParameter("@monto",monto),
                new SqlParameter("@periodo",periodo),
                new SqlParameter("@sede",sede),
                new SqlParameter("@usuarioRegistro",idUsuarioActivo)
            };

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<AuxiliarString> resultado = new List<AuxiliarString>();
                    resultado = _appdbContext.auxiliarStrings
                        .FromSqlRaw("CajaWeb.sp_registrarConceptosLibres @idPersona,@concepto,@monto,@periodo,@sede,@usuarioRegistro", parameters).ToList();

                    scope.Complete();
                    return Json(resultado);
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        [HttpDelete]
        public JsonResult eliminarRegistro(int idConceptoLibre)
        {
            var parameters = new[]
           {
               // new SqlParameter("@returnopcion",System.Data.SqlDbType.Int) { Direction=System.Data.ParameterDirection.Output},
                new SqlParameter("@idConceptoLibre",idConceptoLibre),
            };
            try
            {
                _appdbContext.Database.ExecuteSqlRaw("exec CajaWeb.sp_eliminarConceptoLibre @idConceptoLibre", parameters);
                return Json('1');
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

    }
}
