using CajaSistema.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,ACADEMICO,TESORERO,CAJERO,MARKETING,SECRESEDE")]
    public class BusquedaPasswordsController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;
        public BusquedaPasswordsController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BusquedaAlumno(string texto){

            var parameters = new[]
            {
                new SqlParameter("@opcion","BuscarCla"),
                new SqlParameter("@idperiodo",""),
                new SqlParameter("@codNivelCurso",""),
                new SqlParameter("@cadenabuscar",texto)
            };

            var listaTransacciones = _appdbContext.usuarioPasswords.FromSqlRaw("NetCore.sp_PromocionEdad_Listas @opcion,@idperiodo,@codNivelCurso,@cadenabuscar", parameters).ToList();

            return View(listaTransacciones);
        }

    }
}
