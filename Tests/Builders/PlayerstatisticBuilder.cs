using Models.ModelsBL;

namespace Tests.Builders
{
    public class PlayerstatisticBuilder
    {
        private int StatisticsId;
        private int Numberofwashers;
        private int Averagegametime;

        public PlayerstatisticBuilder()
        {
            StatisticsId = 0;
            Numberofwashers = 0;
            Averagegametime = 0;
        }

        public PlayerstatisticBuilder WithStatisticsId(int StatisticsId)
        {
            this.StatisticsId = StatisticsId;
            return this;
        }
        
        public PlayerstatisticBuilder WithNumberofwashers(int Numberofwashers)
        {
            this.Numberofwashers = Numberofwashers;
            return this;
        }
        
        public PlayerstatisticBuilder WithAveragegametime(int Averagegametime)
        {
            this.Averagegametime = Averagegametime;
            return this;
        }

        public PlayerstatisticBL Build()
        {
            return new PlayerstatisticBL()
            {
                StatisticsId = this.StatisticsId,
                Numberofwashers = this.Numberofwashers,
                Averagegametime = this.Averagegametime
            };
        }
    }
}