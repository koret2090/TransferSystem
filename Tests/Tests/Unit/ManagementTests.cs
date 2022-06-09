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
    public class ManagementTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();
            var data = new List<ManagementBL>();
            managementRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllNotNull()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();
            var data = new List<ManagementBL>()
            {
                new ManagementBuilder()
                    .WithManagementId(1)
                    .WithAnalysistId(1)
                    .WithManagerId(1)
                    .Build()
            };

            managementRepository
                .Setup(x => x.GetAll())
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.GetAll();

            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].ManagementId, result[i].ManagementId);
                Assert.Equal(data[i].AnalysistId, result[i].AnalysistId);
                Assert.Equal(data[i].ManagerId, result[i].ManagerId);
            }
        }

        [Fact]
        public void TestFindByAnalyticNull()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();

            const int id = 1;
            var data = new List<ManagementBL>();

            managementRepository
                .Setup(x => x.FindByAnalytic(id))
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.FindByAnalytic(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindByAnalytic()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();

            const int id = 1;
            var data = new List<ManagementBL>()
            {
                new ManagementBuilder()
                    .WithAnalysistId(id)
                    .Build()
            };

            managementRepository
                .Setup(x => x.FindByAnalytic(id))
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.FindByAnalytic(id);

            // Assert
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].AnalysistId, result.AnalysistId);
            }
        }

        [Fact]
        public void TestFindByManagerNull()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();

            const int id = 1;
            var data = new List<ManagementBL>();

            managementRepository
                .Setup(x => x.FindByManager(id))
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.FindByManager(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindByManager()
        {
            // Arrange
            var managementRepository = new Mock<IManagementRepository>();

            const int id = 1;
            var data = new List<ManagementBL>()
            {
                new ManagementBuilder()
                    .WithManagerId(id)
                    .Build()
            };

            managementRepository
                .Setup(x => x.FindByManager(id))
                .Returns(data);

            var managementController = new ManagementController(managementRepository.Object);

            // Act
            var result = managementController.FindByManager(id);

            // Assert
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].ManagerId, result.ManagerId);
            }
        }
    }
}
