
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteDataProveedor.Controllers;

namespace DatosGTMWeb.Filters
{
    public class CustomAuthenticationFilter: ActionFilterAttribute //Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor httpContext;

        public CustomAuthenticationFilter()
        {
            this.httpContext = new HttpContextAccessor();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var action = filterContext.RouteData.Values.Values.ToList()[1].ToString();
                if (action == "UserRegister")
                    return;

                base.OnActionExecuting(filterContext);

                if (string.IsNullOrEmpty(this.httpContext.HttpContext.Session.GetString("UsuarioLogin")))
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Index");
                    }

                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}
