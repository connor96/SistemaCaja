using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CajaSistema.Filters
{
    public class SessionCheckAtribute:ActionFilterAttribute
    {

        private readonly string _sessionKey;

        // Puedes excluir rutas completas si lo deseas
        private readonly string[] _excludedPaths = { "/Identity/Account/Login", "/Identity/Account/Register", "/Identity/Account/ForgotPassword","/Home" };


        public SessionCheckAtribute(string sessionKey)
        {
            _sessionKey = sessionKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.Value;

            // Excluir rutas que pertenecen al sistema de Identity
            if (_excludedPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
            {
                base.OnActionExecuting(context);
                return;
            }

            var sessionValue = context.HttpContext.Session.GetString(_sessionKey);

            if (string.IsNullOrEmpty(sessionValue))
            {
                // Redirige a la ruta por defecto de Identity
                context.Result = new RedirectResult("/Identity/Account/Login");
                return;
            }

            base.OnActionExecuting(context);
        }

    }
}
