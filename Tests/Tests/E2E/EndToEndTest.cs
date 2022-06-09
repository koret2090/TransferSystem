using System.Collections.Generic;
using Models.ModelsBL;
using Models.ModelsDB;
using Tests.Builders;
using Xunit;

namespace Tests.Tests.E2E
{
    public class EndToEndTest : IClassFixture<TransferSystemAccessObject>
    {
        private readonly TransferSystemAccessObject _accessObject;
        
        public EndToEndTest(TransferSystemAccessObject transferSystemAccessObject)
        {
            _accessObject = transferSystemAccessObject;
        }

        [Fact]
        public void E2ETest()
        {
            for (int i = 0; i < 500; i++)
            {
                ClickBtnTest();
            }
        }
        
        public void ClickBtnTest()
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
            var allPlayers = _accessObject.PlayerRepository.GetAll();
            
            // Assert
            Assert.NotNull(allPlayers);
            
            
            // Arrange
            const int id = 1;
            
            // Act
            var neededPlayer = _accessObject.PlayerRepository.FindPlayerById(id);
            
            // Act
            Assert.Equal(id, neededPlayer.PlayerId);
            
            
            // Arrange
            const int managementId = 5;
            const int desiredPlayerId = 1;
            var desiredPlayer = new DesiredPlayerBuilder()
                .WithId(desiredPlayerId)
                .WithManagementId(managementId)
                .WithPlayerId(neededPlayer.PlayerId)
                .Build();
            
            // Act
            var result = _accessObject.DesiredPlayersRepository.Add(desiredPlayer);
            
            // Assert
            Assert.True(result);
            
            // Arrange
            const int addedPlayerId = 1;
            
            // Act
            var addedDesiredPlayer = _accessObject.DesiredPlayersRepository.GetPlayerById(addedPlayerId);
            
            // Assert
            Assert.NotNull(addedDesiredPlayer);
            Assert.Equal(desiredPlayerId, addedDesiredPlayer.Id);
            Assert.Equal(managementId, addedDesiredPlayer.Managementid);
            Assert.Equal(neededPlayer.PlayerId, addedDesiredPlayer.PlayerId);
            
            _accessObject.TransfersystemContext.Players.RemoveRange(_accessObject.TransfersystemContext.Players);
            _accessObject.TransfersystemContext.Desiredplayers.RemoveRange(_accessObject.TransfersystemContext.Desiredplayers);
            _accessObject.TransfersystemContext.SaveChanges();
        }
    }
}