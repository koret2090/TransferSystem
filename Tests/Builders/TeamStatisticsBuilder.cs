using Models.ModelsBL;

namespace Tests.Builders
{
    public class TeamStatisticsBuilder
    {
        private int statisticsId;
        private int numberOfMatchesPlayed;
        private int league;
        private int placeInTheLeague;
        private int numberOfTrophies;

        public TeamStatisticsBuilder()
        {
            statisticsId = 0;
            numberOfMatchesPlayed = 0;
            league = 0;
            placeInTheLeague = 0;
            numberOfTrophies = 0;
        }

        public TeamStatisticsBuilder WithStatisticsId(int statisticsId)
        {
            this.statisticsId = statisticsId;
            return this;
        }
        
        public TeamStatisticsBuilder WithNumberOfMatchesPlayed(int numberOfMatchesPlayed)
        {
            this.numberOfMatchesPlayed = numberOfMatchesPlayed;
            return this;
        }
        
        public TeamStatisticsBuilder WithLeague(int league)
        {
            this.league = league;
            return this;
        }
        
        public TeamStatisticsBuilder WithPlaceInTheLeague(int placeInTheLeague)
        {
            this.placeInTheLeague = placeInTheLeague;
            return this;
        }
        
        public TeamStatisticsBuilder WithNumberOfTrophies(int numberOfTrophies)
        {
            this.numberOfTrophies = numberOfTrophies;
            return this;
        }

        public TeamstatisticBL Build()
        {
            return new TeamstatisticBL()
            {
                StatisticsId = this.statisticsId,
                NumberOfMatchesPlayed = this.numberOfMatchesPlayed,
                League = this.league,
                PlaceInTheLeague = this.placeInTheLeague,
                NumberOfTrophies = this.numberOfTrophies
            };
        }
    }
}