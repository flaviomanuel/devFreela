using Azure.Core;
using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
            _dbContext = dbContext;
        }

        public async Task<int> CreateProjectAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);

            await _dbContext.SaveChangesAsync();


            return project.Id;
        }

        public async Task<List<Project>> GetAllAsync(string query)
        {
            IQueryable<Project> projects =  _dbContext.Projects;

            if(!string.IsNullOrWhiteSpace(query)){
                projects = projects
                                .Where(x => 
                                            x.Title.Contains(query) ||
                                            x.Description.Contains(query));
            }

            return await projects.ToListAsync();
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
        var result =  await _dbContext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

        return result;
        }
        public async Task CreateCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.Comments.AddAsync(projectComment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt where Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, Id = project.Id });
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
