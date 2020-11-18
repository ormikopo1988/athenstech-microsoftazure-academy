using System.Threading.Tasks;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Configuration.Web.Pages
{
    public class CreateProjectModel : PageModel
    {
        private readonly IProjectService projectService;

        public CreateProjectModel(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectDto Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await projectService.CreateAsync(Project);

            return RedirectToPage("./GetAllProjects");
        }
    }
}
