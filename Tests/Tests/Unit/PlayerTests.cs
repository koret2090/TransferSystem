using System;
using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class PlayerTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();
            var data = new List<PlayerBL>();
            playerRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllNotNull()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();
            var data = new List<PlayerBL>()
            {
                new PlayerBuilder()
                    .WithPlayerId(1)
                    .WithTeamId(1)
                    .WithPlayerStatistics(1)
                    .WithPlayerSpecifications(1)
                    .WithName("name")
                    .WithPosition("pos")
                    .WithWeight(1)
                    .WithHeight(1)
                    .WithNumber(1)
                    .WithAge(1)
                    .WithCountry("country")
                    .WithCost(1)
                    .Build()
            };

            playerRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.GetAll();

            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].PlayerId, result[i].PlayerId);
                Assert.Equal(data[i].TeamId, result[i].TeamId);
                Assert.Equal(data[i].Playerstatistics, result[i].Playerstatistics);
                Assert.Equal(data[i].Playerspecifications, result[i].Playerspecifications);
                Assert.Equal(data[i].Name, result[i].Name);
                Assert.Equal(data[i].Position, result[i].Position);
                Assert.Equal(data[i].Weight, result[i].Weight);
                Assert.Equal(data[i].Height, result[i].Height);
                Assert.Equal(data[i].Number, result[i].Number);
                Assert.Equal(data[i].Age, result[i].Age);
                Assert.Equal(data[i].Country, result[i].Country);
                Assert.Equal(data[i].Cost, result[i].Cost);
            }
        }

        [Fact]
        public void TestFindPlayerByIdNull()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();

            const int id = 1;
            PlayerBL data = null;

            playerRepository
                .Setup(x => x.FindPlayerById(id))
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.FindPlayerById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindPlayerById()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();

            const int id = 1;
            PlayerBL data = new PlayerBuilder()
                .WithPlayerId(id)
                .Build();

            playerRepository
                .Setup(x => x.FindPlayerById(id))
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.FindPlayerById(id);

            // Assert
            Assert.Equal(data.PlayerId, result.PlayerId);
        }

        [Fact]
        public void TestFindPlayerByNameNull()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();

            const string name = "name";
            var data = new List<PlayerBL>();

            playerRepository
                .Setup(x => x.FindPlayerByName(name))
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.FindPlayerByName(name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindPlayerByName()
        {
            // Arrange
            var playerRepository = new Mock<IPlayerRepository>();

            const string name = "name";
            var data = new List<PlayerBL>()
            {
                new PlayerBuilder()
                    .WithName(name)
                    .Build()
            };

            playerRepository
                .Setup(x => x.FindPlayerByName(name))
                .Returns(data);

            var playerController = new PlayerController(playerRepository.Object);

            // Act
            var result = playerController.FindPlayerByName(name);

            // Assert
            Assert.Equal(data[0].Name, result.Name);
        }
    }
}
