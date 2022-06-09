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
    public class FunctionTests
    {
        [Fact]
        public void TestGetPlayersTeamStatNull()
        {
            // Arrange
            var functionsRepository = new Mock<IFunctionsRepository>();
            var data = new List<PlayersTeamStatBL>();
            functionsRepository
                .Setup(x => x.GetPlayersTeamStat())
                .Returns(data);

            var functionController = new FunctionController(functionsRepository.Object);

            // Act
            var result = functionController.GetPlayersTeamStat();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestGetPlayersTeamStatNotNull()
        {
            // Arrange
            var functionsRepository = new Mock<IFunctionsRepository>();
            var data = new List<PlayersTeamStatBL>()
            {
                new PlayersTeamStatBuilder()
                    .WithPlayerId(1)
                    .WithPlayer("name")
                    .WithTeam("team")
                    .WithWashers(1)
                    .WithGameTime(1)
                    .Build()
            };

            functionsRepository
                .Setup(x => x.GetPlayersTeamStat())
                .Returns(data);

            var functionController = new FunctionController(functionsRepository.Object);

            // Act
            var result = functionController.GetPlayersTeamStat();

            // Assert
            Assert.Equal(data.Count, result.Count);
            for (var i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].PlayerId, result[i].PlayerId);
                Assert.Equal(data[i].Player, result[i].Player);
                Assert.Equal(data[i].Team, result[i].Team);
                Assert.Equal(data[i].Washers, result[i].Washers);
                Assert.Equal(data[i].Gametime, result[i].Gametime);
            }
        }
    }
}
