using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingOne.AspNetCore.Samples.Registration.Models;
using PingOne.Core.Management.Services;
using PingOne.Core.Models;

namespace PingOne.AspNetCore.Samples.Registration.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IManagementApiClient _managementApiClient;

        public ManagementController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> CreateUserAsync()
        {
            var model = new CreateUserViewModel
            {
                PasswordRegex = await _managementApiClient.GetPasswordRegexPattern(),
                AllPopulations = await GetPopulationsListItemsAsync()
            };

            return View("CreateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserViewModel model)
        {
            var passwordPattern = await _managementApiClient.GetPasswordRegexPattern();
            
            if (!Regex.IsMatch(model.Password, passwordPattern))
            {
                ModelState.AddModelError("Password", "Password does not match pattern");
            }

            if (ModelState.IsValid)
            {
                var user = await _managementApiClient.CreateUserAsync(
                    new User
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Population = new Population { Id = model.Population}
                    });

                await _managementApiClient.SetPassword(user.Id, model.Password);

                return RedirectToAction("Index", "Home");
            }

            model.AllPopulations = await GetPopulationsListItemsAsync();
            return View("CreateUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            var model = new PasswordRecoveryViewModel
            {
                PasswordRegex = await _managementApiClient.GetPasswordRegexPattern()
            };
            return View("ForgotPassword", model);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(PasswordRecoveryViewModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                RedirectToAction("PasswordRecoveryAsync", model);
            }

            var user = await _managementApiClient.FindUserAsync(model.Username);
            model.Id = user.Id;
            await _managementApiClient.SendPasswordRecoveryCode(user.Id);

            ModelState.Clear();
            return View("PasswordRecovery", model);
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> PasswordRecoveryAsync(PasswordRecoveryViewModel model)
        {
            var passwordPattern = await _managementApiClient.GetPasswordRegexPattern();

            if (!Regex.IsMatch(model.Password, passwordPattern))
            {
                ModelState.AddModelError("Password", "Password does not match pattern");
            }

            if (ModelState.IsValid)
            {
                await _managementApiClient.SetPasswordWithRecoveryCode(
                    model.Id,
                    model.RecoveryCode,
                    model.Password);

                return RedirectToAction("Index", "Home");
            }

            return View("PasswordRecovery", model);
        }

        public async Task<IActionResult> ResendCode(PasswordRecoveryViewModel model)
        {
            await _managementApiClient.SendPasswordRecoveryCode(model.Id);
            ModelState.Clear();
            return View("PasswordRecovery", model);
        }

        private async Task<List<SelectListItem>> GetPopulationsListItemsAsync()
        {
            var populations = await _managementApiClient.GetPopulations();
            return populations.Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList();
        }
    }
}
