
using Microsoft.AspNetCore.Mvc;
using ModeloDataProveedor.DataModel;
using NegocioDataProveedor.IServices;
using Newtonsoft.Json;
using SiteDataProveedor.Models;

namespace SiteDataProveedor.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUsuarioService _usuarioService;
        private Usuario usuario;

        public LoginController( IHttpContextAccessor httpContext, IUsuarioService usuarioService)
        {
            this._httpContext = httpContext;
            this._usuarioService = usuarioService;
            this.usuario = new Usuario();

            if (!string.IsNullOrEmpty(_httpContext.HttpContext.Session.GetString("UsuarioLogin")))
                this.usuario = JsonConvert.DeserializeObject<Usuario>(_httpContext.HttpContext.Session.GetString("UsuarioLogin"));
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LoginUsuario(string userMail, string password)
        {
            var respuesta = new RespuestaModel();
            respuesta.Estado = false;
            respuesta.Mensaje = "No autorizado";
            if (string.IsNullOrEmpty(userMail) || string.IsNullOrEmpty(password))
                return Json(respuesta);

            var passwordEncriptado = NegocioDataProveedor.Helpers.Helper.EnCodeBase64(userMail + password);

            try
            {
                var gestor = this._usuarioService.GetUserData(userMail, passwordEncriptado);
                if (gestor != null)
                {
                    respuesta.Estado = true;
                    respuesta.Mensaje = "Autorizado";
                    _httpContext.HttpContext.Session.SetString("UsuarioLogin", JsonConvert.SerializeObject(gestor));
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return Json(respuesta);
        }

    }
}
