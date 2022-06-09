using Models.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    class PlayerBuilder
    {
        private int playerId;
        private int teamId;
        private int playerstatistics;
        private int playerspecifications;
        private string name;
        private string position;
        private int weight;
        private int height;
        private int number;
        private int age;
        private string country;
        private int cost;

        public PlayerBuilder()
        {
            playerId = 0;
            teamId = 0;
            playerstatistics = 0;
            playerspecifications = 0;
            name = "";
            position = "";
            weight = 0;
            height = 0;
            number = 0;
            age = 0;
            country = "";
            cost = 0;
        }
        public PlayerBuilder WithPlayerId(int playerId)
        {
            this.playerId = playerId;
            return this;
        }

        public PlayerBuilder WithTeamId(int teamId)
        {
            this.teamId = teamId;
            return this;
        }

        public PlayerBuilder WithPlayerStatistics(int playerStatistics)
        {
            this.playerstatistics = playerStatistics;
            return this;
        }

        public PlayerBuilder WithPlayerSpecifications(int playerSpecifications)
        {
            this.playerspecifications = playerSpecifications;
            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public PlayerBuilder WithPosition(string position)
        {
            this.position = position;
            return this;
        }

        public PlayerBuilder WithWeight(int weight)
        {
            this.weight = weight;
            return this;
        }

        public PlayerBuilder WithHeight(int height)
        {
            this.height = height;
            return this;
        }

        public PlayerBuilder WithNumber(int number)
        {
            this.number = number;
            return this;
        }

        public PlayerBuilder WithAge(int age)
        {
            this.age = age;
            return this;
        }

        public PlayerBuilder WithCountry(string country)
        {
            this.country = country;
            return this;
        }

        public PlayerBuilder WithCost(int cost)
        {
            this.cost = cost;
            return this;
        }

        public PlayerBL Build()
        {
            return new PlayerBL()
            {
                PlayerId = this.playerId,
                TeamId = this.teamId,
                Playerstatistics = this.playerstatistics,
                Playerspecifications = this.playerspecifications,
                Name = this.name,
                Position = this.position,
                Weight = this.weight,
                Height = this.height,
                Number = this.number,
                Age = this.age,
                Country = this.country,
                Cost = this.cost
            };
        }
    }
}