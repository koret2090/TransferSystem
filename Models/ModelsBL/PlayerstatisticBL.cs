using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class PlayerstatisticBL
    {
        public int StatisticsId { get; set; }
        public int Numberofwashers { get; set; }
        public int Averagegametime { get; set; }

        public PlayerstatisticBL(Playerstatistic statistic)
        {
            StatisticsId = statistic.Statisticsid;
            Numberofwashers = statistic.Numberofwashers;
            Averagegametime = statistic.Averagegametime;
        }
        
        public PlayerstatisticBL()
        {
            
        }
    }
}
