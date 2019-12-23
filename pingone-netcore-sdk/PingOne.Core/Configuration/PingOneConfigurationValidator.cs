using System;

namespace PingOne.Core.Configuration
{
    public static class PingOneConfigurationValidator
    {
        public static void ValidateManagementConfiguration(PingOneConfigurationManagement configuration)
        {
            ValidateBaseConfiguration(configuration);

            if (string.IsNullOrEmpty(configuration.ApiBaseUrl))
            {
                throw new ArgumentNullException(
                    nameof(configuration.ApiBaseUrl),
                    "Api base URL configuration parameter is missing.");
            }
        }

        public static void ValidateAuthenticationConfiguration(PingOneConfigurationAuthentication configuration)
        {
            ValidateBaseConfiguration(configuration);

            if (string.IsNullOrEmpty(configuration.RedirectPath))
            {
                throw new ArgumentNullException(
                    nameof(configuration.RedirectPath),
                    "Redirect path configuration parameter is missing.");
            }

            if (configuration.Scopes is null || configuration.Scopes.Length <= 0)
            {
                throw new ArgumentNullException(
                    nameof(configuration.Scopes),
                    "Scopes configuration parameter is missing or empty.");
            }
        }

        private static void ValidateBaseConfiguration(PingOneConfigurationBase configuration)
        {
            if (string.IsNullOrEmpty(configuration.AuthBaseUrl))
            {
                throw new ArgumentNullException(
                    nameof(configuration.AuthBaseUrl),
                    "Authentication base URL configuration parameter is missing.");
            }

            if (string.IsNullOrEmpty(configuration.EnvironmentId))
            {
                throw new ArgumentNullException(
                    nameof(configuration.EnvironmentId),
                    "Environment ID configuration parameter is missing.");
            }

            if (string.IsNullOrEmpty(configuration.ClientId))
            {
                throw new ArgumentNullException(
                    nameof(configuration.ClientId),
                    "Client ID configuration parameter is missing.");
            }

            if (string.IsNullOrEmpty(configuration.Secret))
            {
                throw new ArgumentNullException(
                    nameof(configuration.Secret),
                    "Client secret configuration parameter is missing.");
            }
        }
    }
}
