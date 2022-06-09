using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace Models.ModelsDB
{
    [Keyless]
    public class PlayersTeamStat
    {
        public int playerid { get; set; }
        public string player { get; set; }
        public string team { get; set; }
        public int washers { get; set; }
        public int gametime { get; set; }
        
        public PlayersTeamStat(PlayersTeamStatBL playersTeamStat)
        {
            playerid = playersTeamStat.PlayerId;
            player = playersTeamStat.Player;
            team = playersTeamStat.Team;
            washers = playersTeamStat.Washers;
            gametime = playersTeamStat.Gametime;
        }

        public PlayersTeamStat()
        {
            
        }
    }
}
