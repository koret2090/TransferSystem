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
    public class AvailableDealsTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();
            var data = new List<AvailabledealBL>();
            availableDealsRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllNotNull()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();
            var data = new List<AvailabledealBL>()
            {
                new AvailableDealsBuilder()
                    .WithId(1)
                    .WithPlayerId(1)
                    .WithToManagmentId(2)
                    .WithFromManagmentId(1)
                    .WithCost(1000)
                    .WithStatus(1).
                    Build()
            };

            availableDealsRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.GetAll();

            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Id, result[i].Id);
                Assert.Equal(data[i].PlayerId, result[i].PlayerId);
                Assert.Equal(data[i].TomanagementId, result[i].TomanagementId);
                Assert.Equal(data[i].FrommanagementId, result[i].FrommanagementId);
                Assert.Equal(data[i].Cost, result[i].Cost);
                Assert.Equal(data[i].Status, result[i].Status);
            }
        }

        [Fact]
        public void TestGetDealByIdNull()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();

            const int id = 1;
            AvailabledealBL data = null;

            availableDealsRepository
                .Setup(x => x.GetDealById(id))
                .Returns(data);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.GetDealById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetDealById()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();

            const int id = 1;
            AvailabledealBL data = new AvailableDealsBuilder()
                .WithId(id)
                .Build();

            availableDealsRepository
                .Setup(x => x.GetDealById(id))
                .Returns(data);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.GetDealById(id);

            // Assert
            Assert.Equal(data.Id, result.Id);
        }

        [Fact]
        public void TestUpdateNull()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();

            const int id = -1;
            AvailabledealBL data = new AvailableDealsBuilder()
                .WithId(id)
                .Build();

            availableDealsRepository
                .Setup(x => x.Update(data))
                .Returns(false);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.Update(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestUpdate()
        {
            // Arrange
            var availableDealsRepository = new Mock<IAvailableDealsRepository>();

            const int id = 1;
            AvailabledealBL data = new AvailableDealsBuilder()
                .WithId(id)
                .Build();

            availableDealsRepository
                .Setup(x => x.Update(data))
                .Returns(true);

            var availableDealsController = new AvailableDealsController(availableDealsRepository.Object);

            // Act
            var result = availableDealsController.Update(data);

            // Assert
            Assert.True(result);
        }
    }
}
