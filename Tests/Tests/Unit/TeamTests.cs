using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class TeamTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            var data = new List<TeamBL>();
            teamRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            var data = new List<TeamBL>()
            {
                new TeamBuilder().Build()
            };
            
            teamRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.GetAll();
            
            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].TeamId, result[i].TeamId);
                Assert.Equal(data[i].ManagementId, result[i].ManagementId);
                Assert.Equal(data[i].StatisticsId, result[i].StatisticsId);
                Assert.Equal(data[i].Name, result[i].Name);
                Assert.Equal(data[i].Headcoach, result[i].Headcoach);
                Assert.Equal(data[i].Country, result[i].Country);
                Assert.Equal(data[i].Stadium, result[i].Stadium);
                Assert.Equal(data[i].Balance, result[i].Balance);
            }
        }
        
        [Fact]
        public void TestFindTeamByIdNull()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            
            const int id = 1;
            TeamBL data = null;

            teamRepository
                .Setup(x => x.FindTeamById(id))
                .Returns(data);

            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.FindTeamById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindTeamById()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            
            const int id = 1;
            var data = new TeamBuilder()
                .WithTeamid(id)
                .Build();

            teamRepository
                .Setup(x => x.FindTeamById(id))
                .Returns(data);

            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.FindTeamById(id);
            
            // Assert
            Assert.Equal(id, result.TeamId);
        }
        
        [Fact]
        public void TestFindTeamByNameNull()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            
            const string name = "123";
            var data = new List<TeamBL>();
            
            teamRepository
                .Setup(x => x.FindTeamByName(name))
                .Returns(data);

            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.FindTeamByName(name);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindTeamByName()
        {
            // Arrange
            var teamRepository = new Mock<ITeamRepository>();
            
            const string name = "123";
            var team = new TeamBuilder()
                .WithName(name)
                .Build();
            var data = new List<TeamBL>() {team};
            
            teamRepository
                .Setup(x => x.FindTeamByName(name))
                .Returns(data);

            var teamController = new TeamController(teamRepository.Object);
            
            // Act
            var result = teamController.FindTeamByName(name);
            
            // Assert
            Assert.Equal(name, result.Name);
        }
    }
}