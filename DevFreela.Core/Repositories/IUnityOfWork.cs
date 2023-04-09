namespace DevFreela.Core.Repositories
{
    public interface IUnityOfWork
    {
        IProjectRepository Projects {get;}
        IUserRepository Users {get;}
        ISkillRepository Skills {get;}
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}