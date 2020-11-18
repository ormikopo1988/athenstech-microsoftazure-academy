using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<int> CreateAsync(ProjectDto project)
        {
            await projectRepository.SaveAsync(new DbAccess.Entities.Project
            {
                Name = project.Name,
                Description = project.Description
            });

            return await projectRepository.CommitAsync();
        }

        public async Task<List<ProjectDto>> FetchAllAsync()
        {
            var result = new List<ProjectDto>();

            var projectsFromDb = await projectRepository.GetAllAsync();

            if (projectsFromDb != null)
            {
                foreach (var projectFromDb in projectsFromDb)
                {
                    result.Add(new ProjectDto
                    {
                        Name = projectFromDb.Name,
                        Description = projectFromDb.Description
                    });
                }
            }

            return result;
        }
    }
}
