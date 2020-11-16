using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Configuration.Web.Pages
{
    public class ReadSimpleValueModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ReadSimpleValueModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string MyKey { get; private set; }

        public void OnGet()
        {
            // Get config values with IConfiguration
            MyKey = _configuration["MyKey"];
        }
    }
}
