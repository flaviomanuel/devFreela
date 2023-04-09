using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExists_Executed_ReturnThreePRojectViewModel()
        {
            // Arrange

            var projects = new PaginationResult<Project>{
                Data = new List<Project>{
                new Project("Nome teste1", "descricao 1", 1,2,100),
                new Project("Nome teste2", "descricao 2", 1,2,200),
                new Project("Nome teste3", "descricao 2", 1,2,300)
                }
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

        var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1}; 
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(paginationProjectViewModelList);
            Assert.NotEmpty(paginationProjectViewModelList.Data);
            Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

            projectRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}