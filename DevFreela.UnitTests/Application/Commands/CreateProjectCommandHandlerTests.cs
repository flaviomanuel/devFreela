using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //Arrange
            
            var unityOfWorkMock =  new Mock<IUnityOfWork>();

            var projectRepositoryMock = new Mock<IProjectRepository>();

            var skillRepository = new Mock<ISkillRepository>();

            unityOfWorkMock.SetupGet(x => x.Projects).Returns(projectRepositoryMock.Object);
            unityOfWorkMock.SetupGet(x => x.Skills).Returns(skillRepository.Object);

            var createProjectCommand =  new CreateProjectCommand(){
                Title="Title teste", 
                Description= "Nice description",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(unityOfWorkMock.Object);

            //Act

            var id  = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //Assert 
            Assert.True(id >= 0);
            
            projectRepositoryMock.Verify(x => x.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}