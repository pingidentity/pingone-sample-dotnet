using System.Threading.Tasks;
using PingOne.Core.Models;

namespace PingOne.Core.Management.Services
{
    public interface IPingOneTokenProvider
    {
        Task<AuthenticationData> GetAuthenticationData(string clientId, string secret);
    }
}