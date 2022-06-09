using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class TeamStatisticsTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();
            var data = new List<TeamstatisticBL>();
            teamStatisticsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();
            var data = new List<TeamstatisticBL>()
            {
                new TeamStatisticsBuilder().Build()
            };
            
            teamStatisticsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetAll();
            
            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].StatisticsId, result[i].StatisticsId);
                Assert.Equal(data[i].NumberOfMatchesPlayed, result[i].NumberOfMatchesPlayed);
                Assert.Equal(data[i].League, result[i].League);
                Assert.Equal(data[i].PlaceInTheLeague, result[i].PlaceInTheLeague);
                Assert.Equal(data[i].NumberOfTrophies, result[i].NumberOfTrophies);
            }
        }
        
        [Fact]
        public void TestGetStatisticsByTeamNull()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();
            
            var team = new TeamBuilder().Build();
            var data = new List<TeamstatisticBL>();
            
            teamStatisticsRepository
                .Setup(x => x.GetStatisticsByTeam(team))
                .Returns(data);

            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetStatisticsByTeam(team);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetStatisticsByTeam()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();
            
            var team = new TeamBuilder().Build();
            var teamStatistic = new TeamStatisticsBuilder()
                .WithStatisticsId((int)team.StatisticsId)
                .Build();
            var data = new List<TeamstatisticBL>()
            {
                teamStatistic
            };
            
            teamStatisticsRepository
                .Setup(x => x.GetStatisticsByTeam(team))
                .Returns(data);

            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetStatisticsByTeam(team);
            
            // Assert
            Assert.Equal(teamStatistic.StatisticsId, result.StatisticsId);
            Assert.Equal(teamStatistic.NumberOfMatchesPlayed, result.NumberOfMatchesPlayed);
            Assert.Equal(teamStatistic.League, result.League);
            Assert.Equal(teamStatistic.PlaceInTheLeague, result.PlaceInTheLeague);
            Assert.Equal(teamStatistic.NumberOfTrophies, result.NumberOfTrophies);
        }
        
        [Fact]
        public void TestGetTeamStatisticByIdNull()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();

            const int id = 1;
            TeamstatisticBL data = null;
            
            teamStatisticsRepository
                .Setup(x => x.GetTeamStatisticById(id))
                .Returns(data);

            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetTeamStatisticById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetTeamStatisticById()
        {
            // Arrange
            var teamStatisticsRepository = new Mock<ITeamStatisticsRepository>();

            const int id = 1;
            var data = new TeamStatisticsBuilder()
                .WithStatisticsId(id)
                .Build();
            
            teamStatisticsRepository
                .Setup(x => x.GetTeamStatisticById(id))
                .Returns(data);

            var teamStatisticsController = new TeamStatisticsController(teamStatisticsRepository.Object);
            
            // Act
            var result = teamStatisticsController.GetTeamStatisticById(id);
            
            // Assert
            Assert.Equal(data.StatisticsId, result.StatisticsId);
            Assert.Equal(data.NumberOfMatchesPlayed, result.NumberOfMatchesPlayed);
            Assert.Equal(data.League, result.League);
            Assert.Equal(data.PlaceInTheLeague, result.PlaceInTheLeague);
            Assert.Equal(data.NumberOfTrophies, result.NumberOfTrophies);
        }
    }
}