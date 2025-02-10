using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class RegisterController : Controller
    {
        private readonly IUserStore<UserIdentity> _userStore;
        private readonly IUserEmailStore<UserIdentity> _emailStore;
        private readonly UserManager<UserIdentity> _userManager;

        private readonly ApplicationDbContext _appdbContext;
        public RegisterController(ApplicationDbContext context, UserManager<UserIdentity> userManager,
            IUserStore<UserIdentity> userStore)
        {
            _appdbContext = context;
            _userManager = userManager;
            _userStore = userStore;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var listaUsuarios = _appdbContext.Users.ToList();

            var listaRoles = _appdbContext.Roles.ToList();

            ViewBag.Roles = listaRoles;
            ViewBag.Users = listaUsuarios;

            return View();
        }

        [HttpPost]
        public JsonResult buscarPersona(string idpersona)
        {
            var persona=_appdbContext.personaPersona.Where(x=>x.IdPersona==idpersona).ToList();
            return Json(persona);
        }

        [HttpPost]
        public  async Task<IActionResult> Registrar(UserIdentity usuario)
        {

            var rol = _appdbContext.Roles.Where(x => x.Id == usuario.rol).ToList();

            string rolTexto="";

            foreach (var item in rol)
            {
                 rolTexto = item.Name;
            }


            if (rolTexto != "")
            {
                usuario.UserName = usuario.Email;

                var result = await _userManager.CreateAsync(usuario,usuario.password);
                var result2 = await _userManager.AddToRoleAsync(usuario, rolTexto);
                return RedirectToAction("Index","Register");
            
            }
            else
            {
                throw new Exception();
            }

        }
       
    }
}
