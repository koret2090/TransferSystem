using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class TeamBL
    {
        public int TeamId { get; set; }
        public int? ManagementId { get; set; }
        public int? StatisticsId { get; set; }
        public string Name { get; set; }
        public string Headcoach { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public int Balance { get; set; }

        public TeamBL(Team team)
        {
            TeamId = team.Teamid;
            ManagementId = team.Managementid;
            StatisticsId = team.Statisticsid;
            Name = team.Name;
            Headcoach = team.Headcoach;
            Country = team.Country;
            Stadium = team.Stadium;
            Balance = team.Balance;
        }
        
        public TeamBL()
        {
            
        }
    }
}
