

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
