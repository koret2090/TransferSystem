using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
        }

        public int Teamid { get; set; }
        public int? Managementid { get; set; }
        public int? Statisticsid { get; set; }
        public string Name { get; set; }
        public string Headcoach { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public int Balance { get; set; }

        public virtual Management Management { get; set; }
        public virtual Teamstatistic Statistics { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        
        public Team(TeamBL team)
        {
            Teamid = team.TeamId;
            Managementid = team.ManagementId;
            Statisticsid = team.StatisticsId;
            Name = team.Name;
            Headcoach = team.Headcoach;
            Country = team.Country;
            Stadium = team.Stadium;
            Balance = team.Balance;
        }
    }
}
