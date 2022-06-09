using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Playerstatistic
    {
        public Playerstatistic()
        {
            Players = new HashSet<Player>();
        }

        public int Statisticsid { get; set; }
        public int Numberofwashers { get; set; }
        public int Averagegametime { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        
        public Playerstatistic(PlayerstatisticBL statistic)
        {
            Statisticsid = statistic.StatisticsId;
            Numberofwashers = statistic.Numberofwashers;
            Averagegametime = statistic.Averagegametime;
        }
    }
}
