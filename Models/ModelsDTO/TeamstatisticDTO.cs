using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class TeamstatisticDTO
    {
        public int StatisticsId { get; set; }
        public int NumberOfMatchesPlayed { get; set; }
        public int League { get; set; }
        public int PlaceInTheLeague { get; set; }
        public int NumberOfTrophies { get; set; }

        public TeamstatisticDTO()
        {
            
        }
        
        public TeamstatisticDTO(TeamstatisticBL teamstatistic)
        {
            StatisticsId = teamstatistic.StatisticsId;
            NumberOfMatchesPlayed = teamstatistic.NumberOfMatchesPlayed;
            League = teamstatistic.League;
            PlaceInTheLeague = teamstatistic.PlaceInTheLeague;
            NumberOfTrophies = teamstatistic.NumberOfTrophies;
        }

    }
}
