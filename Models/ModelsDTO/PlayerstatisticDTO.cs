using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class PlayerstatisticDTO
    {
        public int StatisticsId { get; set; }
        public int Numberofwashers { get; set; }
        public int Averagegametime { get; set; }

        public PlayerstatisticDTO()
        {
            
        }

        public PlayerstatisticDTO(PlayerstatisticBL statistic)
        {
            StatisticsId = statistic.StatisticsId;
            Numberofwashers = statistic.Numberofwashers;
            Averagegametime = statistic.Averagegametime;
        }
    }
}
