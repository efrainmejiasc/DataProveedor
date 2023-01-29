
using ModeloDataProveedor.DataModel;
using ModeloDataProveedor.IRepositories;
using NegocioDataProveedor.Helpers;
using NegocioDataProveedor.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioDataProveedor.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository _usuarioRepository)
        {
            this.usuarioRepository = _usuarioRepository;
        }

        public Usuario GetUserData( string userMail, string password)
        {
            var usuario = new Usuario();
            usuario = this.usuarioRepository.GetUserData(userMail, password);

            return usuario;
        }

        public string ExisteUsuario(string userName , string email)
        {
            var respuesta = string.Empty;
            var usuario = new Usuario();
            usuario = this.usuarioRepository.GetUserDataBy(userName, email);
            if (usuario != null)
            {
                if (userName == usuario.UserName && email == usuario.Email)
                    respuesta = "El usuario ya se encuentra registrado";
                else if (userName == usuario.UserName && email != usuario.Email)
                    respuesta = "El nombre de usuario no esta disponible";
                else if (userName != usuario.UserName && email == usuario.Email)
                    respuesta = "El email ya se encuentra registrado";
            }

            return respuesta;
        }

        public Usuario PostUsuario(Usuario usuario)
        {
            usuario.Fecha = DateTime.Now;
            usuario.RolId = 3;
            usuario.Estado = true;
            usuario.Password = Helper.EnCodeBase64(usuario.UserName + usuario.Password);
            usuario.Password2 = Helper.EnCodeBase64(usuario.Email + usuario.Password);
            return this.usuarioRepository.PostUsuario(usuario);
        }

    }
}
