using eBay.ApiClient.Auth.OAuth2;
using Ebay_Authentication_Token;
using Ebay_Authentication_Token.Interfaces;
using System;
using System.Configuration;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.Configure<EbayConfiguration>(builder.Configuration.GetSection("EbayConfig"));
builder.Services.AddSingleton<OAuth2Api>();
builder.Services.AddSingleton<IEbayTokenProvider, EbayOAuthTokenProvider>();
builder.Services.AddSingleton<IOAuthConfiguration, OAuthConfiguration>();


var app = builder.Build();

// Enable authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
