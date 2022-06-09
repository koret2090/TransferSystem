using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class PlayersTeamStatDTO
    {
        public int PlayerId { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public int Washers { get; set; }
        public int Gametime { get; set; }

        public PlayersTeamStatDTO()
        {
            
        }

        public PlayersTeamStatDTO(PlayersTeamStatBL playersTeamStat)
        {
            PlayerId = playersTeamStat.PlayerId;
            Player = playersTeamStat.Player;
            Team = playersTeamStat.Team;
            Washers = playersTeamStat.Washers;
            Gametime = playersTeamStat.Gametime;
        }
    }
}
