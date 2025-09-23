using AspNetCoreGeneratedDocument;
using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CajaSistema.Controllers
{
    public class CalendarAdminController : Controller
    {
        public string idUsuarioActivo = "0000000001";
        private readonly ApplicationDbContext _appdbContext;

        public CalendarAdminController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int anioActual = DateTime.Now.Year;
            List<int> ultimosAnios = Enumerable.Range(anioActual - 2, 3)
                                               .OrderByDescending(x => x)
                                               .ToList();

            List<(int Codigo, string Mes)> meses = new List<(int, string)>();
            for (int i = 1; i <= 12; i++)
            {
                string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                meses.Add((i, nombreMes));
            }

            ViewBag.ultimosAnios= ultimosAnios;
            ViewBag.listaMeses = meses;

            return View();
        }

        [HttpPost]
        public PartialViewResult generarReporte(int mes, int anio)
        {
            var parameters = new[]
            {
                new SqlParameter("@mes", mes),
                new SqlParameter("@anio", anio)
            };

            var reporte = from calendario in _appdbContext.calendarioReportes
                .FromSqlRaw("EXEC CajaWeb.sp_ReporteActividadesMes @mes, @anio", parameters)
                .ToList()
                          join persona in _appdbContext.personaPersona.ToList()
                          on calendario.Personal equals persona.IdPersona
                          select new
                          {
                              persona.IdPersona,
                              persona.ApellidoPaterno,
                              persona.Nombres1,
                              calendario.Dia1,
                              calendario.Dia2,
                              calendario.Dia3,
                              calendario.Dia4,
                              calendario.Dia5,
                              calendario.Dia6,
                              calendario.Dia7,
                              calendario.Dia8,
                              calendario.Dia9,
                              calendario.Dia10,
                              calendario.Dia11,
                              calendario.Dia12,
                              calendario.Dia13,
                              calendario.Dia14,
                              calendario.Dia15,
                              calendario.Dia16,
                              calendario.Dia17,
                              calendario.Dia18,
                              calendario.Dia19,
                              calendario.Dia20,
                              calendario.Dia21,
                              calendario.Dia22,
                              calendario.Dia23,
                              calendario.Dia24,
                              calendario.Dia25,
                              calendario.Dia26,
                              calendario.Dia27,
                              calendario.Dia28,
                              calendario.Dia29,
                              calendario.Dia30,
                              calendario.Dia31
                          };

            ViewBag.mes = mes;
            ViewBag.anio = anio;
            return PartialView(reporte);

        }

    }
}
