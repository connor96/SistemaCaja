using CajaSistema.Data;
using CajaSistema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;

namespace CajaSistema.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class RegisterController : Controller
    {
        private readonly IUserStore<UserIdentity> _userStore;
        private readonly IUserEmailStore<UserIdentity> _emailStore;
        private readonly UserManager<UserIdentity> _userManager;
        private string idUsuarioActivo="0000000001";

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

        [HttpPost]
        public PartialViewResult modalRegister(string id)
        {

            UserIdentity personaRegistro = _appdbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            var roles = _appdbContext.Roles.ToList();

            ViewBag.roles = roles;

            return PartialView(personaRegistro);
        }

        [HttpPost]
        public JsonResult editarRegistro(string idPersona, string rol, bool activo, bool inactivo)
        {

            UserIdentity personal = _appdbContext.Users.Where(x => x.idPersona == idPersona).SingleOrDefault();

            string idPersonaNet=personal.Id;

            personal.rol = rol;
            if (inactivo)
            {
                personal.LockoutEnabled = false;
                personal.LockoutEnd = DateTime.Now.AddYears(200);
            }

            IdentityUserRole<string> rolPersona = _appdbContext.UserRoles.Where(x => x.UserId == idPersonaNet).SingleOrDefault();

            IdentityRole rolTexto = _appdbContext.Roles.Where(x => x.Id == rol).SingleOrDefault();


            try
            {
                _appdbContext.Users.Update(personal);
                

                _appdbContext.UserRoles.Remove(rolPersona);


                _appdbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    UserId = rolPersona.UserId,
                    RoleId = rol,
                });

                _appdbContext.SaveChanges();

                return Json("ok");

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
 
        }


        [HttpPost]
        public JsonResult eliminarRegistro(string idRegistro)
        {
            var user= _appdbContext.Users.Where(x => x.Id == idRegistro).SingleOrDefault();
            var rol= _appdbContext.UserRoles.Where(x => x.UserId == idRegistro).SingleOrDefault();

            try
            {
                _appdbContext.UserRoles.Remove(rol);
                _appdbContext.Users.Remove(user);
                _appdbContext.SaveChanges();

                return Json("ok");


            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
           




    }
}
