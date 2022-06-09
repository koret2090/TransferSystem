using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public int? ManagementId { get; set; }
        public int? StatisticsId { get; set; }
        public string Name { get; set; }
        public string Headcoach { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public int Balance { get; set; }

        public TeamDTO()
        {
            
        }
        

        public TeamDTO(TeamBL team)
        {
            TeamId = team.TeamId;
            ManagementId = team.ManagementId;
            StatisticsId = team.StatisticsId;
            Name = team.Name;
            Headcoach = team.Headcoach;
            Country = team.Country;
            Stadium = team.Stadium;
            Balance = team.Balance;
        }
    }
}
