using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Role2.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        [Authorize(Policy = "SellerRead")]
        public IActionResult Seller()
        {
            return View();
        }
    }
}
