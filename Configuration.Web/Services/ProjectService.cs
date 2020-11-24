using Configuration.Web.DbAccess.Entities;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        // This telemetryclient can be used to track additional telemetry using TrackXXX() api.
        private readonly TelemetryClient telemetryClient;

        public ProjectService(IProjectRepository projectRepository, TelemetryClient telemetryClient)
        {
            this.projectRepository = projectRepository;
            this.telemetryClient = telemetryClient;
        }

        public async Task<int> CreateAsync(ProjectDto project)
        {
            await projectRepository.SaveAsync(new Project
            {
                Name = project.Name,
                Description = project.Description
            });

            var projectInsertedRows = await projectRepository.CommitAsync();

            if (projectInsertedRows == 1)
            {
                var customEventProperties = new Dictionary<string, string>();
                customEventProperties.Add("ProjectName", project.Name);

                telemetryClient.TrackEvent("NewProjectCreated", customEventProperties);
            }

            return projectInsertedRows;
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
