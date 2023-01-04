using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> GetDetailsByIdAsync(int id);
        Task<int> CreateProjectAsync(Project project);
        Task CreateCommentAsync(ProjectComment projectComment);
        Task StartAsync(Project project);
        Task SaveChangesAsync();
    }
}
