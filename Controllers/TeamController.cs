using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class TeamController
    {
        private readonly ITeamRepository _teamRepository;
        
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public bool Add(TeamBL team)
        {
            return _teamRepository.Add(team);
        }

        public List<TeamBL> GetAll()
        {
            var teams = _teamRepository.GetAll();
            return teams.Any() ? teams.ToList() : null;
        }

        public bool Update(TeamBL team)
        {
            return _teamRepository.Update(team);
        }

        public StatusCode Delete(int id)
        {
            return _teamRepository.Delete(id);
        }

        public TeamBL FindTeamById(int id)
        {
            return _teamRepository.FindTeamById(id);
        }

        public TeamBL FindTeamByName(string name)
        {
            var team = _teamRepository.FindTeamByName(name);
            return team.Any() ? team.First() : null;
        }
        
        public TeamBL FindTeamByPlayer(PlayerBL player)
        {
            var team = _teamRepository.FindTeamByPlayer(player);
            return team.Any() ? team.First() : null;
        }
        
        public TeamBL FindTeamByManagement(int managementID)
        {
            var team = _teamRepository.FindTeamByManagement(managementID);
            return team.Any() ? team.First() : null;
        }
    }
}