using ClientMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientMVC.Controllers;

[Route("[controller]")]
public class SiteController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SiteController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("[action]")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> Secret()
    {
        //var jsonToken = await HttpContext.GetTokenAsync("access_token");
        //var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jsonToken);

        var model = new ClaimManager(HttpContext, User);

        try
        { 
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.AccessToken);
            var stringAsync = await client.GetStringAsync("https://localhost:5001/site/secret");

            ViewBag.Message = stringAsync;
        }
        catch (Exception exception)
        {
            ViewBag.Message = exception.Message; 
        }

        return View(model);
    }

    [Authorize(Policy = "HasDateOfBirth")]
    [Route("[action]")]
    public async Task<IActionResult> Secret1()
    {
        var model = new ClaimManager(HttpContext, User);

        return View(model);
    }

    [Authorize(Policy = "OlderThan")]
    [Route("[action]")]
    public async Task<IActionResult> Secret2()
    {
        var model = new ClaimManager(HttpContext, User);

        return View(model);
    }
}
