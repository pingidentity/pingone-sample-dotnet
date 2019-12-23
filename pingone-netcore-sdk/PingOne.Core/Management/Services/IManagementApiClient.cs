using System.Collections.Generic;
using System.Threading.Tasks;
using PingOne.Core.Models;

namespace PingOne.Core.Management.Services
{
    public interface IManagementApiClient
    {
        Task<User> FindUserAsync(string userName);

        Task<User> CreateUserAsync(User user);

        Task<List<Population>> GetPopulations();

        Task<List<PasswordPolicy>> GetPasswordPolicies();

        Task<string> GetPasswordRegexPattern();

        Task SendPasswordRecoveryCode(string userId);

        Task SetPasswordWithRecoveryCode(string userId, string recoveryCode, string newPassword);

        Task SetPassword(string userId, string password);
    }
}
