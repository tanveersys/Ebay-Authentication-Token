Efficient eBay OAuth Token Provider for ASP.NET Core

**Description:**

This document details the EbayOAuthTokenProvider class, designed to streamline the process of obtaining and managing OAuth access tokens for interacting with the eBay API within your ASP.NET Core application.

**Dependency**
install the required dependency: Install-Package eBay.OAuth.Client

## Key Features

* **Simplified Token Retrieval:** Dedicated methods streamline acquiring access tokens for various grant flows, such as Client Credentials and Authorization Code.
* **Enhanced Maintainability:** Core token fetching logic is segregated from application-specific logic, promoting cleaner code organization.
* **Optional Caching:** This class offers the flexibility to implement in-memory or external caching for potentially faster token retrieval (implementation guidance provided).
* **Clean Code & Dependency Injection:** The code adheres to a clean structure and utilizes dependency injection, facilitating testing and integration.

**Usage:**
1.Dependency Injection:
builder.Services.AddSingleton<IOAuthConfiguration>(config => new OAuthConfiguration(config.GetSection("EbayConfig")));
builder.Services.AddSingleton<OAuth2Api>(/* Implementation to create OAuth2Api instance */); // Replace with actual logic
builder.Services.AddSingleton<IEbayTokenProvider, EbayOAuthTokenProvider>();

2.Injecting the Token Provider:
Inject IEbayTokenProvider into your controllers or services where you need to interact with the eBay API:

public class ProductsController : ControllerBase
{
    private readonly IEbayTokenProvider _tokenProvider;

    public ProductsController(IEbayTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        // 1. Get the access token
        var tokenResponse = await _tokenProvider.GetAccessTokenAsync();
        if (tokenResponse?.AccessToken?.Token == null)
        {
            return BadRequest("Failed to retrieve access token");
        }

        // 2. Construct your API request with the access token
        string accessToken = tokenResponse.AccessToken.Token;
        string baseUrl = "https://api.ebay.com/v1/"; // Replace with specific API endpoint URL
        string resource = "sell/listing"; // Replace with specific resource endpoint

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync(baseUrl + resource);

            if (response.IsSuccessStatusCode)
            {
                // Process the API response
                var content = await response.Content.ReadAsStringAsync();
                // ...
                return Ok(content);
            }
            else
            {
                return StatusCode(response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}

Configuration
Configure the EbayConfig section in your appsettings.json file to provide the following credentials:
Environment: The eBay environment (e.g., "sandbox" or "production")
ClientId: Your eBay application's client ID
CertId: Your eBay application's certificate ID (optional for some flows)
DevId: Your eBay developer ID
RedirectUri: The redirect URI configured in your eBay application settings (optional for Authorization Code Grant)
