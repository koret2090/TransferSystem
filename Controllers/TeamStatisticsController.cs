using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class TeamStatisticsController
    {
        private readonly ITeamStatisticsRepository _teamStatisticsRepository;
        
        public TeamStatisticsController(ITeamStatisticsRepository teamStatisticsRepository)
        {
            _teamStatisticsRepository = teamStatisticsRepository;
        }

        public bool Add(TeamstatisticBL teamstatistic)
        {
            return _teamStatisticsRepository.Add(teamstatistic);
        }

        public List<TeamstatisticBL> GetAll()
        {
            var teamStatistic = _teamStatisticsRepository.GetAll();
            return teamStatistic.Any() ? teamStatistic.ToList() : null;
        }

        public bool Update(TeamstatisticBL teamstatistic)
        {
            return _teamStatisticsRepository.Update(teamstatistic);
        }

        public StatusCode Delete(int id)
        {
            return _teamStatisticsRepository.Delete(id);
        }

        public TeamstatisticBL GetTeamStatisticById(int id)
        {
            return _teamStatisticsRepository.GetTeamStatisticById(id);
        }

        public TeamstatisticBL GetStatisticsByTeam(TeamBL team)
        {
            var teamstatistics = _teamStatisticsRepository.GetStatisticsByTeam(team);
            return teamstatistics.Any() ? teamstatistics.First() : null;
        }
    }
}