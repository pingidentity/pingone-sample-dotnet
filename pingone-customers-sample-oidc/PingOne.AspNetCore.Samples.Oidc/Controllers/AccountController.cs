using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PingOne.AspNetCore.Samples.Oidc.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login()
        {
            await HttpContext.ChallengeAsync("PingOne", new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") });
        }

        [Authorize(AuthenticationSchemes = "PingOne")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("PingOne", new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
