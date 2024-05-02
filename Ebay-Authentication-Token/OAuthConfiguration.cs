using eBay.ApiClient.Auth.OAuth2.Model;
using Ebay_Authentication_Token.Interfaces;

namespace Ebay_Authentication_Token
{
    public class OAuthConfiguration : IOAuthConfiguration
    {
        private readonly IConfiguration _configuration;

        public OAuthConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OAuthEnvironment GetEnvironment()
        {
            var environment = _configuration.GetValue<string>("EbayConfig:Environment");
            return environment.ToLower() switch
            {
                "sandbox" => OAuthEnvironment.SANDBOX,
                "production" => OAuthEnvironment.PRODUCTION,
                _ => throw new ArgumentException($"Invalid eBay environment: {environment}")
            };
        }
    }
}
