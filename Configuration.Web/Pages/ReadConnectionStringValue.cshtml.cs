using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Configuration.Web.Pages
{
    public class ReadConnectionStringValueModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ReadConnectionStringValueModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string PrimaryDBConnectionString { get; private set; }
        public string SecondaryDBConnectionString { get; private set; }

        public void OnGet()
        {
            // Handy extension method for reading connection string from configuration
            PrimaryDBConnectionString = _configuration.GetConnectionString("PrimaryDB");

            // Which is simply a short-hand for
            SecondaryDBConnectionString = _configuration.GetSection("ConnectionStrings")["SecondaryDB"];
        }
    }
}
