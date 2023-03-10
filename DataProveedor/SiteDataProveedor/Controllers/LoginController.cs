
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModeloDataProveedor.DataModel;
using NegocioDataProveedor.Helpers;
using NegocioDataProveedor.IServices;
using Newtonsoft.Json;
using SiteDataProveedor.Models;

namespace SiteDataProveedor.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUsuarioService _usuarioService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Usuario usuario;

        public LoginController( IHttpContextAccessor httpContext, IUsuarioService usuarioService, IWebHostEnvironment webHostEnvironment)
        {
            this._httpContext = httpContext;
            this._usuarioService = usuarioService;
            this._webHostEnvironment = webHostEnvironment;
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
                var logPath = this._webHostEnvironment.WebRootPath + AppEnviroments.log_excepcion;
                Helper.WriteFileLog(logPath, ex.Message);
                respuesta.Mensaje = ex.Message;
            }

            return Json(respuesta);
        }

    }
}
