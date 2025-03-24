using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,TESORERO")]
    public class RedireccionController : Controller
    {
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

            ViewBag.listaCajeros = listaCajeros;

            return View();
        }


        [HttpPost]
        public JsonResult CajerosActivosPeriodo(string periodo)
        {
            var parameter = new[]
            {
                new SqlParameter("@periodo",periodo)
            };

            var listaCajerosActivos = _appdbContext.redireccionCajeros.FromSqlRaw("CajaWeb.sp_listaCajerosActivos @periodo", parameter).ToList();
            
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

            var listaTransacciones = _appdbContext.cajaTransaccionCabecera.FromSqlRaw("CajaWeb.sp_CajaTransaccionCabecera @opcion,@idCajero,@periodo,@sede", parameters).ToList();

            return PartialView(listaTransacciones);
        }

    }
}
