using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class PlayersTeamStatBL
    {
        public int PlayerId { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public int Washers { get; set; }
        public int Gametime { get; set; }
        
        public PlayersTeamStatBL(PlayersTeamStat playersTeamStat)
        {
            PlayerId = playersTeamStat.playerid;
            Player = playersTeamStat.player;
            Team = playersTeamStat.team;
            Washers = playersTeamStat.washers;
            Gametime = playersTeamStat.gametime;
        }

        public PlayersTeamStatBL()
        {
            
        }
    }
}
