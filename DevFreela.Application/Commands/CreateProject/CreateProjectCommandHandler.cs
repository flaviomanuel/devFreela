using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnityOfWork _unityOfWork;
        public CreateProjectCommandHandler(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title,
                request.Description, 
                request.IdClient, 
                request.IdFreelancer,
                request.TotalCost);

            await _unityOfWork.BeginTransactionAsync();

            await _unityOfWork.Projects.CreateProjectAsync(project);


            await _unityOfWork.CompleteAsync();

            await _unityOfWork.Skills.AddSkillFromProject(project);

            await _unityOfWork.CompleteAsync();
            
            await _unityOfWork.CommitAsync();
            
            return project.Id;
        }
    }
}
