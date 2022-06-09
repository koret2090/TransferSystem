using Models.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    class PlayersTeamStatBuilder
    {
        private int playerId;
        private string player;
        private string team;
        private int washers;
        private int gametime;

        public PlayersTeamStatBuilder()
        {
            playerId = 0;
            player = "";
            team = "";
            washers = 0;
            gametime = 0;
        }

        public PlayersTeamStatBuilder WithPlayerId(int playerId)
        {
            this.playerId = playerId;
            return this;
        }

        public PlayersTeamStatBuilder WithPlayer(string player)
        {
            this.player = player;
            return this;
        }

        public PlayersTeamStatBuilder WithTeam(string team)
        {
            this.team = team;
            return this;
        }

        public PlayersTeamStatBuilder WithWashers(int washers)
        {
            this.washers = washers;
            return this;
        }

        public PlayersTeamStatBuilder WithGameTime(int gametime)
        {
            this.gametime = gametime;
            return this;
        }

        public PlayersTeamStatBL Build()
        {
            return new PlayersTeamStatBL()
            {
                PlayerId = this.playerId,
                Player = this.player,
                Team = this.team,
                Washers = this.washers,
                Gametime = this.gametime
            };
        }
    }
}
