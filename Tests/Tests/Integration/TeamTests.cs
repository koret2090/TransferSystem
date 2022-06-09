using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDB;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Integration
{
    [Collection("Integration")]
    public class TeamTests : IClassFixture<TransferSystemAccessObject>
    {
        private readonly TransferSystemAccessObject _accessObject;
        
        public TeamTests(TransferSystemAccessObject transferSystemAccessObject)
        {
            _accessObject = transferSystemAccessObject;
        }
        
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange

            // Act
            var result = _accessObject.TeamRepository.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            List<Team> teams = new List<Team>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new TeamBuilder().WithTeamid(i).Build();
                teams.Add(new Team(curTeam));
            }
            
            _accessObject.TransfersystemContext.Teams.AddRange(teams);
            _accessObject.TransfersystemContext.SaveChanges();
            
            // Act
            var result = _accessObject.TeamRepository.GetAll();
            
            // Assert
            Assert.Equal(teams.Count, result.Count);
            for (var i = 0; i < teams.Count; i++)
            {
                Assert.Equal(teams[i].Teamid, result[i].TeamId);
                Assert.Equal(teams[i].Managementid, result[i].ManagementId);
                Assert.Equal(teams[i].Statisticsid, result[i].StatisticsId);
                Assert.Equal(teams[i].Name, result[i].Name);
                Assert.Equal(teams[i].Headcoach, result[i].Headcoach);
                Assert.Equal(teams[i].Country, result[i].Country);
                Assert.Equal(teams[i].Stadium, result[i].Stadium);
                Assert.Equal(teams[i].Balance, result[i].Balance);
            }
            
            Cleanup();
        }
        
        [Fact]
        public void TestFindTeamByIdNull()
        {
            // Arrange
            const int id = 10;

            // Act
            var result = _accessObject.TeamRepository.FindTeamById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindTeamById()
        {
            // Arrange
            const int id = 1; 
            
            List<Team> teams = new List<Team>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new TeamBuilder().WithTeamid(i).Build();
                teams.Add(new Team(curTeam));
            }
            
            _accessObject.TransfersystemContext.Teams.AddRange(teams);
            _accessObject.TransfersystemContext.SaveChanges();
            
            // Act
            var result = _accessObject.TeamRepository.FindTeamById(id);
            
            // Assert
            Assert.Equal(id, result.TeamId);
            
            Cleanup();
        }
        
        [Fact]
        public void TestFindTeamByNameNull()
        {
            // Arrange
            const string name = "aboba";

            // Act
            var result = _accessObject.TeamRepository.FindTeamByName(name);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindTeamByName()
        {
            // Arrange
            const string name = "aboba";
            const string resName = "aboba1"; 
            
            List<Team> teams = new List<Team>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new TeamBuilder().WithTeamid(i).WithName(name + i).Build();
                teams.Add(new Team(curTeam));
            }
            
            _accessObject.TransfersystemContext.Teams.AddRange(teams);
            _accessObject.TransfersystemContext.SaveChanges();
            
            // Act
            var result = _accessObject.TeamRepository.FindTeamByName(resName);
            
            // Assert
            Assert.Equal(resName, result[0].Name);
            
            Cleanup();
        }
        
        private void Cleanup()
        {
            _accessObject.TransfersystemContext.Teams.RemoveRange(_accessObject.TransfersystemContext.Teams);
            _accessObject.TransfersystemContext.SaveChanges();
        }
    }
}