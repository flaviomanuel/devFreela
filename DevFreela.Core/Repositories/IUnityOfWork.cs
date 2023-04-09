namespace DevFreela.Core.Repositories
{
    public interface IUnityOfWork
    {
        IProjectRepository Projects {get;}
        IUserRepository Users {get;}
        Task<int> CompleteAsync();
    }
}