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
    public class DesiredPlayersTests
    {

        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();
            var data = new List<DesiredplayerBL>();
            desiredPlayersRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllNotNull()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();
            var data = new List<DesiredplayerBL>()
            {
                new DesiredPlayerBuilder()
                    .WithId(1)
                    .WithPlayerId(1)
                    .WithManagementId(1)
                    .Build()
            };
            
            desiredPlayersRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetAll();

            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Id, result[i].Id);
                Assert.Equal(data[i].PlayerId, result[i].PlayerId);
                Assert.Equal(data[i].Managementid, result[i].Managementid);                
            }
        }

        [Fact]
        public void TestGetPlayerByIdNull()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();

            const int id = 1;
            DesiredplayerBL data = null;

            desiredPlayersRepository
                .Setup(x => x.GetPlayerById(id))
                .Returns(data);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetPlayerById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetDealById()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();

            const int id = 1;
            DesiredplayerBL data = new DesiredPlayerBuilder()
                .WithId(id)
                .Build();

            desiredPlayersRepository
                .Setup(x => x.GetPlayerById(id))
                .Returns(data);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetPlayerById(id);

            // Assert
            Assert.Equal(data.Id, result.Id);
        }

        [Fact]
        public void TestGetPlayersByManagementNull()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();

            var playersData = new List<DesiredplayerBL>();
            ManagementBL managementData = null;

            desiredPlayersRepository
                .Setup(x => x.GetPlayersByManagement(managementData))
                .Returns(playersData);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetPlayersByManagement(managementData);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetPlayersByManagement()
        {
            // Arrange
            var desiredPlayersRepository = new Mock<IDesiredPlayersRepository>();

            const int id = 1;
            var playersData = new List<DesiredplayerBL>()
            {
                new DesiredPlayerBuilder()
                    .WithId(1)
                    .WithPlayerId(1)
                    .WithManagementId(1)
                    .Build()
            };

            var managerData = new ManagementBuilder()
                .WithManagementId(1)
                .Build();

            desiredPlayersRepository
                .Setup(x => x.GetPlayersByManagement(managerData))
                .Returns(playersData);

            var desiredPlayersController = new DesiredPlayersController(desiredPlayersRepository.Object);

            // Act
            var result = desiredPlayersController.GetPlayersByManagement(managerData);

            // Assert
            Assert.Equal(playersData.Count, result.Count);
            for (var i = 0; i < playersData.Count; i++)
            {
                Assert.Equal(playersData[i].Id, result[i].Id);
                Assert.Equal(playersData[i].PlayerId, result[i].PlayerId);
                Assert.Equal(playersData[i].Managementid, result[i].Managementid);
            }
        }
    }
}
