using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CajaSistema.Controllers
{

    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,TESORERO,CAJERO,MARKETING,SECRESEDE")]
    public class TalleresController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;
        public TalleresController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var conceptosLista = await _appdbContext.tarifarioOtrosListas.Where(_ => _.tipoConcepto == 8).ToListAsync();
            
            ViewBag.conceptosLista = conceptosLista;
            return View();

        }

        [HttpPost]
        public JsonResult BusquedaAlumnos(string cadena, int idSede, string periodo, string concepto)
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
                new SqlParameter("@concepto",concepto)
            };

            var resultadoBusqueda = _appdbContext.alumnoBusquedas.FromSqlRaw("CajaWeb.sp_busquedaAlumnosGraduaciones @cadenabuscar,@idSede,@concepto", parametro).ToList();
            return Json(resultadoBusqueda);
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarTaller(string idPersona, int concepto)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    var periodoActivo = await _appdbContext.listaPeriodosPeriodos.Where(x => x.estado == true).FirstOrDefaultAsync();

                    idUsuarioActivo = HttpContext.Session.GetString("idPersona");

                    var parameters = new[]
                    {
                        new SqlParameter("@idPersona",idPersona),
                        new SqlParameter("@concepto",concepto),
                        new SqlParameter("@periodo",periodoActivo?.periodoTexto),
                        new SqlParameter("@sede",1),
                        new SqlParameter("@usuarioRegistro",idUsuarioActivo)
                    };

                    List<AuxiliarString> resultado = new List<AuxiliarString>();
                    resultado = _appdbContext.auxiliarStrings.FromSqlRaw("CajaWeb.sp_registrarOtrosConceptos @idPersona,@concepto,@periodo,@sede,@usuarioRegistro", parameters).ToList();
                    scope.Complete();
                    return Json(new { success = true, message = "Taller registrado correctamente" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }

        [HttpGet]
        public PartialViewResult TablaConceptos()
        {

            var tipos = new int[] { 28, 29, 30, 31, 32 };

            var listaConceptos = (from conceptos in _appdbContext.otrosConceptosVarios.Where(x => x.idSede == 1).ToList()
                                  join persona in _appdbContext.personaPersona.ToList()
                                  on conceptos.idPersona equals persona.IdPersona
                                  join per in _appdbContext.listaPeriodosPeriodos
                                  on conceptos.periodo equals per.periodoTexto
                                  join tarifario in _appdbContext.tarifarioOtrosListas.Where(x=>x.tipoConcepto==8).ToList()
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

            return PartialView(listaConceptos);
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


    }
}
