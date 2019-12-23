namespace PingOne.Core.Configuration
{
    public abstract class PingOneConfigurationBase
    {
        public string AuthBaseUrl { get; set; }

        public string EnvironmentId { get; set; }

        public string ClientId { get; set; }

        public string Secret { get; set; }
    }
}