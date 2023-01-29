using ModeloDataProveedor.DataModel;

using Microsoft.EntityFrameworkCore;
using ModeloDataProveedor.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDataProveedor.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly MyAppContext db;
        public UsuarioRepository(MyAppContext _db)
        {
            this.db = _db;
        }
        public Usuario GetUserData( string userMail, string password)
        {
            var usuario = db.Usuario.Where(x => (x.Password == password || x.Password2 == password) 
                                             && (x.Email == userMail || x.UserName == userMail) && x.Estado).FirstOrDefault();
            if(usuario != null)
            {
                usuario.Password = string.Empty;
                usuario.Password2 = string.Empty;
            }
            return usuario;
        }

        public Usuario PostUsuario(Usuario usuario)
        {
            var user = db.Usuario;
            user.Add(usuario);
            db.SaveChanges();

            return usuario;
        }


        public Usuario GetUserDataBy(string userName, string email)
        {
            var usuario = db.Usuario.Where(x => x.Email == email || x.UserName == userName).FirstOrDefault();
            if (usuario != null)
            {
                usuario.Password = string.Empty;
                usuario.Password2 = string.Empty;
            }
            return usuario;
        }

    

    }
}
