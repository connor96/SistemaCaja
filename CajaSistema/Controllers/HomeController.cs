using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CajaSistema.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _appdbContext;

        public ISession Sesion => HttpContext.Session;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _appdbContext = context;
        }

        public IActionResult Index()
        {
            string usuarioPersona = HttpContext.User.Identity.Name;
            string idPersona="";

            var persona = _appdbContext.Users.Where(x => x.UserName == usuarioPersona);
            foreach (var user in persona) {

                idPersona = user.idPersona;
            }

            Sesion.SetString("idPersona", idPersona);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
