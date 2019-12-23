using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PingOne.Core.Models;

namespace PingOne.Core.Management.Services
{
    public class PingOneTokenProvider : IPingOneTokenProvider
    {
        private readonly HttpClient _httpClient;

        public PingOneTokenProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthenticationData> GetAuthenticationData(string clientId, string secret)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", secret)
            };

            var content = new FormUrlEncodedContent(postData);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync("token", content);

            return await response.Content.ReadAsAsync<AuthenticationData>();
        }
    }
}