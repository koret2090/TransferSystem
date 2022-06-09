using Models.ModelsBL;

namespace Tests.Builders
{
    public class TeamBuilder
    {
        private int teamid;
        private int managementid;
        private int statisticsId;
        private string name;
        private string headcoach;
        private string country;
        private string stadium;
        private int balance;

        public TeamBuilder()
        {
            teamid = 0;
            managementid = 0;
            statisticsId = 0;
            name = "";
            headcoach = "";
            country = "";
            stadium = "";
            balance = 0;
        }
        
        public TeamBuilder WithTeamid(int teamid)
        {
            this.teamid = teamid;
            return this;
        }
        
        public TeamBuilder WithManagementId(int managementid)
        {
            this.managementid = managementid;
            return this;
        }
        
        public TeamBuilder WithStatisticsId(int teamid)
        {
            this.teamid = teamid;
            return this;
        }
        
        public TeamBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }
        
        public TeamBuilder WithHeadcoach(string headcoach)
        {
            this.headcoach = headcoach;
            return this;
        }
        
        public TeamBuilder WithCountry(string country)
        {
            this.country = country;
            return this;
        }
        
        public TeamBuilder WithStadium(string stadium)
        {
            this.stadium = stadium;
            return this;
        }
        
        public TeamBuilder WithBalance(int balance)
        {
            this.balance = balance;
            return this;
        }
        
        public TeamBL Build()
        {
            return new TeamBL()
            {
                TeamId = this.teamid,
                ManagementId = this.managementid,
                StatisticsId = this.statisticsId,
                Name = this.name,
                Headcoach = this.headcoach,
                Country = this.country,
                Stadium = this.stadium,
                Balance = this.balance
            };
        }
    }
}