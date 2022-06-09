using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class TeamstatisticBL
    {
        public int StatisticsId { get; set; }
        public int NumberOfMatchesPlayed { get; set; }
        public int League { get; set; }
        public int PlaceInTheLeague { get; set; }
        public int NumberOfTrophies { get; set; }

        public TeamstatisticBL(Teamstatistic teamstatistic)
        {
            StatisticsId = teamstatistic.Statisticsid;
            NumberOfMatchesPlayed = teamstatistic.Numberofmatchesplayed;
            League = teamstatistic.League;
            PlaceInTheLeague = teamstatistic.Placeintheleague;
            NumberOfTrophies = teamstatistic.Numberoftrophies;
        }
        
        public TeamstatisticBL()
        {
            
        }
    }
}
