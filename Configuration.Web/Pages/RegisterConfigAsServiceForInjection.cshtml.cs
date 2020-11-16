using Configuration.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Configuration.Web.Pages
{
    public class RegisterConfigAsServiceForInjectionModel : PageModel
    {
        private readonly OAuthSettings _settings;

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string Scope { get; private set; }
        public string RedirectUrl { get; private set; }

        public RegisterConfigAsServiceForInjectionModel(OAuthSettings settings)
        {
            _settings = settings;
        }

        public void OnGet()
        {
            // Read using Configuration section registered as a service in the DI container
            ClientId = _settings.ClientId;
            ClientSecret = _settings.ClientSecret;
            Scope = _settings.Scope;
            RedirectUrl = _settings.RedirectUrl;
        }
    }
}
