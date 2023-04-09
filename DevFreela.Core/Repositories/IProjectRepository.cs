using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1);
        Task<Project> GetDetailsByIdAsync(int id);
        Task CreateProjectAsync(Project project);
        Task CreateCommentAsync(ProjectComment projectComment);
        Task UpdateAsync(Project project);
        Task StartAsync(Project project);
        Task SaveChangesAsync();
    }
}
