namespace Configuration.Web.Models
{
    public class OAuthSettings
    {
        public const string OAuthSection = "OAuthSettings";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string RedirectUrl { get; set; }
    }
}
