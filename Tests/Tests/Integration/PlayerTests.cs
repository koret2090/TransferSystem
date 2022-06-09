using System;
using System.Collections.Generic;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDB;
using Moq;
using Tests.Builders;
using Tests.Tests.Integration;
using Xunit;

namespace Tests.Tests.Integration
{
    [Collection("Integration")]
    public class PlayerTests : IClassFixture<TransferSystemAccessObject>
    {
        private readonly TransferSystemAccessObject _accessObject;
        
        public PlayerTests(TransferSystemAccessObject transferSystemAccessObject)
        {
            _accessObject = transferSystemAccessObject;
        }
        
        [Fact]
        public void TestGetAllNull()
        {
            // Arrange
            
            // Act
            var result = _accessObject.PlayerRepository.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllNotNull()
        {
            // Arrange
            List<Player> players = new List<Player>();
            for (var i = 1; i < 4; i++)
            {
                var curPlayer = new PlayerBuilder().WithPlayerId(i).Build();
                players.Add(new Player(curPlayer));
            }
            
            _accessObject.TransfersystemContext.Players.AddRange(players);
            _accessObject.TransfersystemContext.SaveChanges();
            
            // Act
            var result = _accessObject.PlayerRepository.GetAll();

            // Assert
            Assert.Equal(players.Count, result.Count);
            for (var i = 0; i < players.Count; i++)
            {
                Assert.Equal(players[i].Playerid, result[i].PlayerId);
            }
            
            Cleanup();
        }

        [Fact]
        public void TestFindPlayerByIdNull()
        {
            // Arrange
            const int id = 1;
            // Act
            var result = _accessObject.PlayerRepository.FindPlayerById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindPlayerById()
        {
            // Arrange
            const int id = 1;
            
            List<Player> players = new List<Player>();
            for (var i = 1; i < 4; i++)
            {
                var curPlayer = new PlayerBuilder().WithPlayerId(i).Build();
                players.Add(new Player(curPlayer));
            }
            
            _accessObject.TransfersystemContext.Players.AddRange(players);
            _accessObject.TransfersystemContext.SaveChanges();

            // Act
            var result = _accessObject.PlayerRepository.FindPlayerById(id);

            // Assert
            Assert.Equal(players[0].Playerid, result.PlayerId);
            
            Cleanup();
        }

        [Fact]
        public void TestFindPlayerByNameNull()
        {
            // Arrange
            const string name = "aboba";

            // Act
            var result = _accessObject.PlayerRepository.FindPlayerByName(name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestFindPlayerByName()
        {
            // Arrange
            const string name = "aboba";
            const string nameRes = "aboba1";
            
            List<Player> players = new List<Player>();
            for (var i = 1; i < 4; i++)
            {
                var curPlayer = new PlayerBuilder().WithPlayerId(i).WithName(name + i).Build();
                players.Add(new Player(curPlayer));
            }
            
            _accessObject.TransfersystemContext.Players.AddRange(players);
            _accessObject.TransfersystemContext.SaveChanges();

            // Act
            var result = _accessObject.PlayerRepository.FindPlayerByName(nameRes);

            // Assert
            Assert.Equal(players[0].Name, result[0].Name);
            
            Cleanup();
        }
        
        private void Cleanup()
        {
            _accessObject.TransfersystemContext.Players.RemoveRange(_accessObject.TransfersystemContext.Players);
            _accessObject.TransfersystemContext.SaveChanges();
        }
    }
}
