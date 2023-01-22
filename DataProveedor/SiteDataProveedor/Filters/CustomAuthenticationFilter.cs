
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
                var action = filterContext.RouteData.Values.Values.ToList();
                if (action[0].ToString() == "Index" && action[1].ToString() == "Usuario")
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
