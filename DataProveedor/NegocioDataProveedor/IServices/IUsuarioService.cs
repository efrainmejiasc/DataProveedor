
using ModeloDataProveedor.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioDataProveedor.IServices
{
    public interface IUsuarioService
    {
        Usuario PostUsuario(Usuario usuario);
        string ExisteUsuario(string userName, string email);
        Usuario GetUserData(string userMail, string password);

    }
}
