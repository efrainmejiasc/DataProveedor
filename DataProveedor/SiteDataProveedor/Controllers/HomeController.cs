using Microsoft.AspNetCore.Mvc;

namespace SiteDataProveedor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
