using DatosGTMWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ModeloDataProveedor.DataModel;
using NegocioDataProveedor.Helpers;
using NegocioDataProveedor.IServices;
using Newtonsoft.Json;
using SiteDataProveedor.Filters;
using SiteDataProveedor.Models;

namespace SiteDataProveedor.Controllers
{
   
    public class UsuarioController : Controller
    {

        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUsuarioService _usuarioService;
        private Usuario usuario;

        public UsuarioController( IHttpContextAccessor httpContext, IUsuarioService usuarioService, IWebHostEnvironment webHostEnvironment)
        {
            this._httpContext = httpContext;
            this._usuarioService = usuarioService;
            this._webHostEnvironment = webHostEnvironment;

            if (!string.IsNullOrEmpty(_httpContext.HttpContext.Session.GetString("UsuarioLogin")))
                this.usuario = JsonConvert.DeserializeObject<Usuario>(_httpContext.HttpContext.Session.GetString("UsuarioLogin"));
        }

        [CustomAuthenticationFilter(Disable = true)]
        public IActionResult RegistroUsuario()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UserRegister([FromBody] Usuario usuario)
        {
            var respuesta = new RespuestaModel();
            respuesta.Estado = false;

            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                respuesta.Mensaje = "Mala solicitud";
                return Json(respuesta);
            }

            try
            {
                usuario = this._usuarioService.PostUsuario(usuario);
                if (usuario.Id > 0)
                {
                    respuesta.Estado = true;
                    respuesta.Mensaje = "Usuario creado correctamente.";
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

        [HttpGet]
        public IActionResult GetUsuarioLogger()
        {
            return Json(this.usuario);
        }

    }
}
