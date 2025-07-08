using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Role2.Controllers
{
    //[Authorize]
    public class ManagerController : Controller
    {
        [Authorize(Policy = "ManagerRead")]
        public IActionResult Manager()
        {
            return View();
        }
    }
}
