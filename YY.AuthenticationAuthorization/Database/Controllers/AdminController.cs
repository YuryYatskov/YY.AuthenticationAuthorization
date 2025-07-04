using Database.ViewModels;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Database.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        //private readonly ApplicationDbContext _context;

        //public AdminController(ApplicationDbContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "AdministratorRead")]
        public IActionResult Administrator()
        {
             return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = await _context.Users.SingleOrDefaultAsync(
            //    x => x.UserName == model.UserName
            //    && x.Password == model.Password);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                //ModelState.AddModelError("UserName", "User not found");
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, model.UserName),
            //    new Claim(ClaimTypes.Role, model.Role)
            //};
            //var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            //var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            //HttpContext.SignInAsync("Cookie", claimPrincipal);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
        
            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            //HttpContext.SignOutAsync("Cookie");
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
