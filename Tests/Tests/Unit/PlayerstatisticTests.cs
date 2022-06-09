using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class PlayerstatisticTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();
            var data = new List<PlayerstatisticBL>();
            playerStatisticsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();
            var data = new List<PlayerstatisticBL>()
            {
                new PlayerstatisticBuilder().Build()
            };
            
            playerStatisticsRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetAll();
            
            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].StatisticsId, result[i].StatisticsId);
                Assert.Equal(data[i].Numberofwashers, result[i].Numberofwashers);
                Assert.Equal(data[i].Averagegametime, result[i].Averagegametime);
            }
        }
        
        [Fact]
        public void TestGetStatisticsByPlayerNull()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();
            
            var player = new PlayerBuilder().Build();
            var data = new List<PlayerstatisticBL>();
            
            playerStatisticsRepository
                .Setup(x => x.GetStatisticsByPlayer(player))
                .Returns(data);

            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetStatisticsByPlayer(player);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetStatisticsByPlayer()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();
            
            var player = new PlayerBuilder().Build();
            var playerStatistic = new PlayerstatisticBuilder()
                .WithStatisticsId((int)player.Playerstatistics)
                .Build();
            var data = new List<PlayerstatisticBL>()
            {
                playerStatistic
            };
            
            playerStatisticsRepository
                .Setup(x => x.GetStatisticsByPlayer(player))
                .Returns(data);

            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetStatisticsByPlayer(player);
            
            // Assert
            Assert.Equal(playerStatistic.StatisticsId, result.StatisticsId);
            Assert.Equal(playerStatistic.Numberofwashers, result.Numberofwashers);
            Assert.Equal(playerStatistic.Averagegametime, result.Averagegametime);
        }
        
        [Fact]
        public void TestGetPlayerStatisticByIdNull()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();

            const int id = 1;
            PlayerstatisticBL data = null;
            
            playerStatisticsRepository
                .Setup(x => x.GetPlayerStatisticById(id))
                .Returns(data);

            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetPlayerStatisticById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetPlayerStatisticById()
        {
            // Arrange
            var playerStatisticsRepository = new Mock<IPlayerStatisticsRepository>();

            const int id = 1;
            var data = new PlayerstatisticBuilder()
                .WithStatisticsId(id)
                .Build();
            
            playerStatisticsRepository
                .Setup(x => x.GetPlayerStatisticById(id))
                .Returns(data);

            var playerStatisticsController = new PlayerStatisticsController(playerStatisticsRepository.Object);
            
            // Act
            var result = playerStatisticsController.GetPlayerStatisticById(id);
            
            // Assert
            Assert.Equal(data.StatisticsId, result.StatisticsId);
            Assert.Equal(data.Numberofwashers, result.Numberofwashers);
            Assert.Equal(data.Averagegametime, result.Averagegametime);
        }
    }
}