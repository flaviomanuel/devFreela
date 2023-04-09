using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;
        private readonly DevFreelaDbContext _context;
        public SkillRepository(IConfiguration configuration, DevFreelaDbContext context)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
            _context = context;
        }

        public async Task AddSkillFromProject(Project project)
        {
            var words = project.Description.Split(' ');
            var length = words.Length;

            var skill= $"{project.Id} - {words[length - 1]}";

            await _context.Skills.AddAsync(new Skill(skill));
        }

        public async Task<List<SkillDTO>> GetSkillAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var script = "SELECT Id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();
            }
        }
    }
}
