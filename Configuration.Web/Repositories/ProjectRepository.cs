using Configuration.Web.DbAccess;
using Configuration.Web.DbAccess.Entities;
using Configuration.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Web.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<Project> projectDbSet;

        public ProjectRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.projectDbSet = applicationDbContext.Set<Project>();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await projectDbSet.ToListAsync();
        }

        public async Task<int> SaveAsync(Project project)
        {
            if (project.IsValid())
            {
                var newProject = await projectDbSet.AddAsync(project);

                return newProject.Entity.Id;
            }

            return 0;
        }

        public async Task<int> CommitAsync()
        {
            return await applicationDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await applicationDbContext.DisposeAsync();
        }
    }
}
