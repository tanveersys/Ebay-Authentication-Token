namespace Ebay_Authentication_Token
{
    public class EbayConfiguration
    {
        public string RedirectUri { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string CertId { get; set; } = string.Empty;
        public string DevId { get; set; } = string.Empty;

        public static EbayConfiguration LoadFromConfiguration(IConfiguration configuration)
        {
            var ebaySection = configuration.GetSection("EbayConfig");
            return new EbayConfiguration
            {
                Environment = ebaySection["Environment"],
                ClientId = ebaySection["ClientId"],
                CertId = ebaySection["CertId"],
                DevId = ebaySection["DevId"],
            };
        }
    }
}
