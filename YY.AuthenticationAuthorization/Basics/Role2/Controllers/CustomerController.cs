using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Role2.Controllers
{
    //[Authorize]
    public class CustomerController : Controller
    {
        [Authorize(Policy = "CustomerRead")]
        public IActionResult Customer()
        {
            return View();
        }
    }
}
