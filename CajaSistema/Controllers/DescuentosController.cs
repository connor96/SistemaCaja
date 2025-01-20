using CajaSistema.Data;
using Microsoft.AspNetCore.Mvc;

namespace CajaSistema.Controllers
{
    public class DescuentosController : Controller
    {
        private readonly ApplicationDbContext _appdbContext;


        public string idUsuarioActivo = "0000000001";

        public DescuentosController(ApplicationDbContext context)
        {
            _appdbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var listDescuentos = from desal in _appdbContext.descuentoDescuento.ToList()
                                 join des in _appdbContext.descuentoListaDescuentos.ToList()
                                 on desal.idDescuentoConcepto equals des.IdDescuento
                                 join persona in _appdbContext.personaPersona.ToList()
                                 on desal.idPersona equals persona.IdPersona
                                 select new
                                 {
                                     desal.idDescuento,
                                     desal.idPersona,
                                     persona.ApellidoPaterno,
                                     persona.ApellidoMaterno,
                                     persona.Nombres1,
                                     persona.Nombres2,
                                     des.DesDescuento,
                                     desal.monto,
                                     desal.estado
                                 };

            ViewBag.listaDescuentos= listDescuentos;
            return View();
        }
    }
}
