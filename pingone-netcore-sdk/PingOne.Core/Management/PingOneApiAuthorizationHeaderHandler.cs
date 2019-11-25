using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PingOne.Core.Configuration;
using PingOne.Core.Management.Services;

namespace PingOne.Core.Management
{
    public class PingOneApiAuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly IPingOneTokenProvider _pingOneTokenProvider;
        private readonly PingOneConfigurationManagement _configuration;
        private readonly IMemoryCache _cache;

        public PingOneApiAuthorizationHeaderHandler(
            IPingOneTokenProvider pingOneTokenProvider,
            PingOneConfigurationManagement configuration,
            IMemoryCache cache)
        {
            _pingOneTokenProvider = pingOneTokenProvider;
            _configuration = configuration;
            _cache = cache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await GetAuthorizationHeaderValue();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetAuthorizationHeaderValue()
        {
            if (_cache.TryGetValue(_configuration.EnvironmentId, out string accessToken))
            {
                return accessToken;
            }

            var auth = await _pingOneTokenProvider.GetAuthenticationData(_configuration.ClientId, _configuration.Secret);
            _cache.Set(_configuration.EnvironmentId, auth.AccessToken, TimeSpan.FromSeconds(auth.ExpiresIn));
            return auth.AccessToken;
        }
    }
}