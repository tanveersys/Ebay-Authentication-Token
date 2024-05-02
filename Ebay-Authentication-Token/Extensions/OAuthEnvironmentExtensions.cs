using eBay.ApiClient.Auth.OAuth2.Model;

namespace Ebay_Authentication_Token.Extensions
{
    public static class OAuthEnvironmentExtensions
    {
        public static OAuthEnvironment GetEnvironment(this EbayConfiguration config)
        {
            switch (config.Environment.ToLower())
            {
                case "sandbox":
                    return OAuthEnvironment.SANDBOX;
                case "production":
                    return OAuthEnvironment.PRODUCTION;
                default:
                    throw new ArgumentException($"Invalid eBay environment: {config.Environment}");
            }
        }
    }
}
