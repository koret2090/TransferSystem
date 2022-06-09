using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class PlayerSpecificationsTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();
            var data = new List<PlayerspecificationBL>();
            playerSpecificationsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();
            var data = new List<PlayerspecificationBL>()
            {
                new PlayerspecificationBuilder().Build()
            };
            
            playerSpecificationsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetAll();
            
            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].SpecificationsId, result[i].SpecificationsId);
                Assert.Equal(data[i].Shooting, result[i].Shooting);
                Assert.Equal(data[i].Defense, result[i].Defense);
                Assert.Equal(data[i].Skating, result[i].Skating);
                Assert.Equal(data[i].Physical, result[i].Physical);
            }
        }
        
        [Fact]
        public void TestGetSpecificationByPlayerNull()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();
            
            var player = new PlayerBuilder().Build();
            var data = new List<PlayerspecificationBL>();
            
            playerSpecificationsRepository
                .Setup(x => x.GetSpecificationByPlayer(player))
                .Returns(data);

            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetSpecificationByPlayer(player);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetSpecificationByPlayer()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();
            
            var player = new PlayerBuilder().Build();
            var playerSpecification = new PlayerspecificationBuilder()
                .WithSpecificationsId((int)player.Playerspecifications)
                .Build();
            var data = new List<PlayerspecificationBL>()
            {
                playerSpecification
            };
            
            playerSpecificationsRepository
                .Setup(x => x.GetSpecificationByPlayer(player))
                .Returns(data);

            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetSpecificationByPlayer(player);
            
            // Assert
            Assert.Equal(playerSpecification.SpecificationsId, result.SpecificationsId);
            Assert.Equal(playerSpecification.Shooting, result.Shooting);
            Assert.Equal(playerSpecification.Defense, result.Defense);
            Assert.Equal(playerSpecification.Skating, result.Skating);
            Assert.Equal(playerSpecification.Physical, result.Physical);
        }
        
        [Fact]
        public void TestGetPlayerSpecificationByIdNull()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();

            const int id = 1;
            PlayerspecificationBL data = null;
            
            playerSpecificationsRepository
                .Setup(x => x.GetPlayerSpecificationById(id))
                .Returns(data);

            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetPlayerSpecificationById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetPlayerSpecificationById()
        {
            // Arrange
            var playerSpecificationsRepository = new Mock<IPlayerSpecificationsRepository>();

            const int id = 1;
            var data = new PlayerspecificationBuilder()
                .WithSpecificationsId(id)
                .Build();
            
            playerSpecificationsRepository
                .Setup(x => x.GetPlayerSpecificationById(id))
                .Returns(data);

            var playerSpecificationsController = new PlayerSpecificationsController(playerSpecificationsRepository.Object);
            
            // Act
            var result = playerSpecificationsController.GetPlayerSpecificationById(id);
            
            // Assert
            Assert.Equal(data.SpecificationsId, result.SpecificationsId);
            Assert.Equal(data.Shooting, result.Shooting);
            Assert.Equal(data.Defense, result.Defense);
            Assert.Equal(data.Skating, result.Skating);
            Assert.Equal(data.Physical, result.Physical);
        }
    }
}