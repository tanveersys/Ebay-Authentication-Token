using eBay.ApiClient.Auth.OAuth2.Model;

namespace Ebay_Authentication_Token.Interfaces
{
    public interface IOAuthConfiguration
    {
        OAuthEnvironment GetEnvironment(); 
    }
}
