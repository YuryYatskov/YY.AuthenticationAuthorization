using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
