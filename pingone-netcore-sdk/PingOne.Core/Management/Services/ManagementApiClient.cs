using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PingOne.Core.Models;

namespace PingOne.Core.Management.Services
{
    public class ManagementApiClient : IManagementApiClient
    {
        private readonly HttpClient _httpClient;

        public ManagementApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("users", user);

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JObject.Parse(jsonResult).ToObject<User>();
        }

        public async Task<User> FindUserAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"users/?filter=email eq \"{userName}\" or username eq \"{userName}\"");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var parsedResult = JObject.Parse(jsonResult);
            return parsedResult["_embedded"]["users"].ToList().FirstOrDefault()?.ToObject<User>();
        }

        public async Task<List<Population>> GetPopulations()
        {
            var response = await _httpClient.GetAsync("populations");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var parsedResult = JObject.Parse(jsonResult);
            var resultPartitions = parsedResult["_embedded"]["populations"].ToList();

            var result = new List<Population>();
            resultPartitions.ForEach(x => result.Add(x.ToObject<Population>()));

            return result;
        }

        public async Task<List<PasswordPolicy>> GetPasswordPolicies()
        {
            var response = await _httpClient.GetAsync("passwordPolicies");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var parsedResult = JObject.Parse(jsonResult);
            var resultPartitions = parsedResult["_embedded"]["passwordPolicies"].ToList();

            var result = new List<PasswordPolicy>();
            resultPartitions.ForEach(x => result.Add(x.ToObject<PasswordPolicy>()));

            return result;
        }

        public async Task<string> GetPasswordRegexPattern()
        {
            var passwordPolicies = await GetPasswordPolicies();
            var defaultPolicy = passwordPolicies.FirstOrDefault(pp => pp.Default);

            var passwordPattern = new StringBuilder("^(?:");

            if (defaultPolicy != null)
            {
                foreach (var (pattern, minValue) in defaultPolicy.MinCharacters)
                {
                    passwordPattern.Append("(?=(?:.*[");
                    passwordPattern.Append(
                        Regex.Replace(
                            pattern,
                            "[\\{\\}\\(\\)\\[\\]\\.\\+\\*\\?\\^\\$\\\\|-]",
                            "\\${0}"));
                    passwordPattern.Append("]){");
                    passwordPattern.Append(minValue);
                    passwordPattern.Append(",})");
                }

                passwordPattern.Append(")");

                passwordPattern.Append("(?!.*(.)\\1{");
                passwordPattern.Append(defaultPolicy.MaxRepeatedCharacters);
                passwordPattern.Append(",}).{");
                passwordPattern.Append($"{defaultPolicy.Length.Min},{defaultPolicy.Length.Max}");
            }

            passwordPattern.Append("}$");

            return passwordPattern.ToString();
        }

        public async Task SendPasswordRecoveryCode(string userId)
        {
            await _httpClient.PostAsync(
                $"users/{userId}/password",
                new StringContent(string.Empty, Encoding.UTF8, "application/vnd.pingidentity.password.sendRecoveryCode+json"));
        }

        public async Task SetPassword(string userId, string password)
        {
            var data = new { forceChange = "false", value = password };

            await _httpClient.PutAsync(
                $"users/{userId}/password",
                data,
                new JsonMediaTypeFormatter(),
                "application/vnd.pingidentity.password.set+json");
        }

        public async Task SetPasswordWithRecoveryCode(string userId, string recoveryCode, string newPassword)
        {
            var data = new { recoveryCode, newPassword };

            await _httpClient.PostAsync(
                $"users/{userId}/password",
                data,
                new JsonMediaTypeFormatter(),
                "application/vnd.pingidentity.password.recover+json");
        }
    }
}
