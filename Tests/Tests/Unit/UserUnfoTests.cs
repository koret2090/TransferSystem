using System;
using System.Collections.Generic;
using ComponentAccessToDB.RepositoryImplementation;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDB;
using Moq;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.Unit
{
    public class UserInfoTests
    {
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();
            var data = new List<UserInfoBL>();
            userInfoRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.GetAll();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();
            var data = new List<UserInfoBL>()
            {
                new UserBuilder()
                    .WithId(1)
                    .WithLogin("123")
                    .WithHash("123")
                    .WithPerms(3)
                    .Build()
            };
            
            userInfoRepository
                .Setup(x => x.GetAll())
                .Returns(data);
            
            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.GetAll();
            
            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Id, result[i].Id);
                Assert.Equal(data[i].Hash, result[i].Hash);
                Assert.Equal(data[i].Login, result[i].Login);
                Assert.Equal(data[i].Permission, result[i].Permission);
            }
        }
        
        [Fact]
        public void TestFindUserByLoginNull()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();
            
            const string login = "123";
            var data = new List<UserInfoBL>();
            
            userInfoRepository
                .Setup(x => x.FindUserByLogin(login))
                .Returns(data);

            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.FindUserByLogin(login);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindUserByLogin()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();
            
            const string login = "123";
            var user = new UserBuilder()
                .WithLogin(login)
                .Build();
            var data = new List<UserInfoBL>() {user};
            
            userInfoRepository
                .Setup(x => x.FindUserByLogin(login))
                .Returns(data);

            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.FindUserByLogin(login);
            
            // Assert
            Assert.Equal(login, result.Login);
        }
        
        [Fact]
        public void TestFindUserByIdNotNull()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();
            
            const int id = 1;
            UserInfoBL data = null;

            userInfoRepository
                .Setup(x => x.FindUserById(id))
                .Returns(data);

            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.FindUserById(id);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void TestFindUserById()
        {
            // Arrange
            var userInfoRepository = new Mock<IUserInfoRepository>();

            const int id = 1;
            var data = new UserBuilder()
                .WithId(id)
                .Build();

            userInfoRepository
                .Setup(x => x.FindUserById(id))
                .Returns(data);

            var userController = new UserInfoController(userInfoRepository.Object);
            
            // Act
            var result = userController.FindUserById(id);
            
            // Assert
            Assert.Equal(id, result.Id);
        }
    }
}