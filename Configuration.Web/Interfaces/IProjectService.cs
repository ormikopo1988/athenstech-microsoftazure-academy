using Configuration.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Web.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> FetchAllAsync();
        Task<int> CreateAsync(ProjectDto project);
    }
}
