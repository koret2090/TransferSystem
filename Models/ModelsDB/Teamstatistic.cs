using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Teamstatistic
    {
        public Teamstatistic()
        {
            Teams = new HashSet<Team>();
        }

        public int Statisticsid { get; set; }
        public int Numberofmatchesplayed { get; set; }
        public int League { get; set; }
        public int Placeintheleague { get; set; }
        public int Numberoftrophies { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        
        public Teamstatistic(TeamstatisticBL teamstatistic)
        {
            Statisticsid = teamstatistic.StatisticsId;
            Numberofmatchesplayed = teamstatistic.NumberOfMatchesPlayed;
            League = teamstatistic.League;
            Placeintheleague = teamstatistic.PlaceInTheLeague;
            Numberoftrophies = teamstatistic.NumberOfTrophies;
        }

    }
}
