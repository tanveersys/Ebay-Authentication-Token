using eBay.ApiClient.Auth.OAuth2;
using eBay.ApiClient.Auth.OAuth2.Model;
using Ebay_Authentication_Token.Interfaces;

namespace Ebay_Authentication_Token
{
    public class EbayOAuthTokenProvider : IEbayTokenProvider
    {
        private readonly IOAuthConfiguration _environmentProvider;
        private readonly OAuth2Api oauthApi;
        public EbayOAuthTokenProvider(IOAuthConfiguration environmentProvider, OAuth2Api oauthApi)
        {
            _environmentProvider = environmentProvider;
            this.oauthApi = oauthApi;
        }

        public OAuthResponse GetAccessTokenAsync()
        {
            var environment = _environmentProvider.GetEnvironment();
            var tokenResponse = oauthApi.GetApplicationToken(environment, null);
            return tokenResponse;
        }
        public OAuthResponse RefreshAccessTokenAsync(string refreshToken, List<string> scopes)
        {
            var environment = _environmentProvider.GetEnvironment();
            return oauthApi.GetAccessToken(environment, refreshToken, scopes);
        }
        public OAuthResponse ExchangeCodeForAccessTokenAsync(string code)
        {
            var environment = _environmentProvider.GetEnvironment();
            return oauthApi.ExchangeCodeForAccessToken(environment, code);
        }
        public string GenerateUserAuthorizationUrlAsync(IList<string> scopes, string state)
        {
            var environment = _environmentProvider.GetEnvironment();
            return oauthApi.GenerateUserAuthorizationUrl(environment, scopes, state);
        }
    }
}
