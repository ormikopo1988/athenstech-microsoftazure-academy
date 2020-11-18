using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Configuration.Web.Pages
{
    public class GetAllProjectsModel : PageModel
    {
        private readonly IProjectService projectService;

        public GetAllProjectsModel(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IEnumerable<ProjectDto> Projects { get; set; }

        public async Task OnGetAsync()
        {
            Projects = await projectService.FetchAllAsync();
        }
    }
}
