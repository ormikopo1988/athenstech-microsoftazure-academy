using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Configuration.Web.Pages
{
    public class ReadHierarchicalValueModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ReadHierarchicalValueModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string PositionTitle { get; private set; }
        public string PositionName { get; private set; }
        public string PositionCountry { get; private set; }
        public string DefaultLogLevel { get; private set; }

        public void OnGet()
        {
            // Get config values with IConfiguration
            PositionTitle = _configuration["Position:Title"];
            PositionName = _configuration["Position:Name"];
            
            // Casting with (optional) default value
            PositionCountry = _configuration.GetValue<string>("Position:Country", "India");

            DefaultLogLevel = _configuration["Logging:LogLevel:Default"];
        }
    }
}
