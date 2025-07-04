using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTBearer.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
        return View();
    }

    public IActionResult Authenticate()
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, "Vita"),
            new Claim(JwtRegisteredClaimNames.Email, "vita@mail.com"),
        };

        byte[] secretKey = Encoding.UTF8.GetBytes(Constans.SecretKey);
        var key = new SymmetricSecurityKey(secretKey);

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            Constans.Issuer,
            Constans.Audience,
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials);

        var value = new JwtSecurityTokenHandler().WriteToken(token);
        ViewBag.Token = value;
        return View();
    }

}
