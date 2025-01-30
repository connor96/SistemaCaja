using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;

        private string idCajeroActivo = "1105240228";
        public TransaccionesController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var parameters = new[]
            {
                new SqlParameter("@opcion",1),
                new SqlParameter("@idCajero",idCajeroActivo),
                new SqlParameter("@periodo",""),
                new SqlParameter("@sede",1)
            };

            var listaTransacciones = _appdbContext.cajaTransaccionCabecera.FromSqlRaw("CajaWeb.sp_CajaTransaccionCabecera", parameters).ToList();

            return View();
        }
    }
}
