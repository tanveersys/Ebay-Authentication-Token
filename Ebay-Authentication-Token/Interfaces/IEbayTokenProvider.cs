using eBay.ApiClient.Auth.OAuth2.Model;
using eBay.ApiClient.Auth.OAuth2;

namespace Ebay_Authentication_Token.Interfaces
{
    public interface IEbayTokenProvider
    {
        OAuthResponse GetAccessTokenAsync();
        OAuthResponse RefreshAccessTokenAsync(string refreshToken, List<string> scopes);
        OAuthResponse ExchangeCodeForAccessTokenAsync(string code);
        string GenerateUserAuthorizationUrlAsync(IList<string> scopes, string state);
    }
}
