namespace PingOne.Core.Configuration
{
    public class PingOneConfigurationAuthentication : PingOneConfigurationBase
    {
        public string ResponseType { get; set; }

        public string RedirectPath { get; set; }

        public string PostSignOffRedirectUrl { get; set; }

        public string[] Scopes { get; set; }
    }
}
