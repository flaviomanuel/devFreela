using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DevFreelaDbContext _context;
        public IProjectRepository Projects { get; }
        public IUserRepository Users { get; }
        public ISkillRepository Skills {get;}

        public UnityOfWork(DevFreelaDbContext context,IProjectRepository projects, IUserRepository users, ISkillRepository skills)
        {
            _context = context;
            Projects = projects;
            Users = users;
            Skills = skills;
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

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex; 
            }
        }
    }
}