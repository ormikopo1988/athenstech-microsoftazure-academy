using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Configuration.Web.Pages
{
    public class GetAllProjectsModel : PageModel
    {
        private readonly IProjectService projectService;
        private readonly ILogger<GetAllProjectsModel> logger;

        public GetAllProjectsModel(IProjectService projectService, ILogger<GetAllProjectsModel> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        public IEnumerable<ProjectDto> Projects { get; set; }

        public async Task OnGetAsync()
        {
            logger.LogWarning("About to fetch all available projects so far.");

            Projects = await projectService.FetchAllAsync();
        }
    }
}
