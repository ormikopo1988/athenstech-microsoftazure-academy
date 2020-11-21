using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Configuration.Web.Pages
{
    public class ReadValueFromArrayModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ReadValueFromArrayModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FirstServerName { get; private set; }

        public void OnGet()
        {
            // From array with index
            FirstServerName = _configuration["Servers:0:Name"];
        }
    }
}
