
using ModeloDataProveedor.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDataProveedor.IRepositories
{
    public interface IUsuarioRepository
    {
        Usuario PostUsuario(Usuario usuario);
        Usuario GetUserDataBy(string userName, string email);
        Usuario GetUserData(string userMail, string password);
    }
}
