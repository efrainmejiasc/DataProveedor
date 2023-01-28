
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteDataProveedor.Controllers;
using SiteDataProveedor.Filters;

namespace DatosGTMWeb.Filters
{
    public class CustomAuthenticationFilter: ActionFilterAttribute //Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor httpContext;
        public bool Disable { get; set; }
        public CustomAuthenticationFilter()
        {
            this.httpContext = new HttpContextAccessor();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                
                base.OnActionExecuting(filterContext);
                var action = filterContext.RouteData.Values.Values.ToList();
                if (action[0].ToString() == "Usuario" && (action[1].ToString() == "RegistroUsuario" || action[1].ToString() == "UserRegister"))
                  return;

                if (string.IsNullOrEmpty(this.httpContext.HttpContext.Session.GetString("UsuarioLogin")))
                {
                    if (filterContext.Controller is LoginController == false )
                           filterContext.HttpContext.Response.Redirect("/Login/Index");
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}
