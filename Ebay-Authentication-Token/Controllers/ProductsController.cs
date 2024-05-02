using Ebay_Authentication_Token.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Ebay_Authentication_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEbayTokenProvider _tokenProvider;

        public ProductsController(IEbayTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<IActionResult> GetProductsAsync()
        {
            // 1. Get Access Token
            var tokenResponse = _tokenProvider.GetAccessTokenAsync();

            if (tokenResponse.AccessToken != null)
            {
                // 2. Use the access token in your API call
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken.Token);

                // Replace with your actual eBay API call URL and logic
                var response = await httpClient.GetAsync("https://api.ebay.com/v1/products");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsStringAsync();
                    return Ok(products);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            else
            {
                return BadRequest("Failed to retrieve access token");
            }
        }
    }
}
