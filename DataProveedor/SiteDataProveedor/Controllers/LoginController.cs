using Microsoft.AspNetCore.Mvc;

namespace SiteDataProveedor.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
