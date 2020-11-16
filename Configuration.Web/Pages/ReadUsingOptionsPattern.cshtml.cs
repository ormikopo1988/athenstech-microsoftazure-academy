using Configuration.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Configuration.Web.Pages
{
    public class ReadUsingOptionsPatternModel : PageModel
    {
        private readonly PositionOptions _options;

        public string PositionTitle { get; private set; }
        public string PositionName { get; private set; }

        public ReadUsingOptionsPatternModel(IOptions<PositionOptions> options)
        {
            _options = options.Value;
        }

        public void OnGet()
        {
            // Read using Options Pattern
            PositionTitle = _options.Title;
            PositionName = _options.Name;
        }
    }
}
