using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientMVC.Controllers;

[Route("[controller]")]
public class SiteController : Controller
{
    [Route("[action]")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [Route("[action]")]
    public IActionResult Secret()
    {
        return View();
    }
}
