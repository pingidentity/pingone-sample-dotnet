using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PingOne.AspNetCore.Samples.Oidc.Models;

namespace PingOne.AspNetCore.Samples.Oidc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "PingOne")]
        public async Task<IActionResult> UserInfoAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("PingOne", "access_token");
            var tokenId = await HttpContext.GetTokenAsync("PingOne", "id_token");
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            ViewData["accessToken"] = accessToken;
            ViewData["tokenId"] = tokenId;
            ViewData["securityToken"] = securityToken;

            var user = new UserInfoViewModel { 
                FamilyName = HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value,
                GivenName = HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value,
                Email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value,
                PreferredUsername = HttpContext.User.FindFirst("preferred_username")?.Value,
                UpdatedAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(HttpContext.User.FindFirst("updated_at").Value)).DateTime.ToLocalTime(),
            };

            return View("UserInfo", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
