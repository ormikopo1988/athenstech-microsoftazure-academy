using Configuration.Web.DbAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Web.Interfaces
{
    public interface IProjectRepository : IAsyncDisposable
    {
        Task<List<Project>> GetAllAsync();
        Task<int> SaveAsync(Project project);
        Task<int> CommitAsync();
    }
}
