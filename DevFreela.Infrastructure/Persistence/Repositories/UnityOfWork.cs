using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly DevFreelaDbContext _context;
        public IProjectRepository Projects { get; }
        public IUserRepository Users { get; }
        public UnityOfWork(DevFreelaDbContext context,IProjectRepository projects, IUserRepository users)
        {
            _context = context;
            Projects = projects;
            Users = users;
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing){
            if(disposing){
                _context.Dispose();
            }
        }

    }
}