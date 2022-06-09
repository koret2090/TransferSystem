using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB;
using Microsoft.Extensions.Logging;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models.ModelsBL;

namespace ComponentBuisinessLogic
{
    public class UserController
    {
        private readonly FunctionController _functionController;
        private readonly PlayerStatisticsController _playerStatisticsController;
        
        protected readonly PlayerController _playerController;
        protected readonly TeamController _teamController;
        protected readonly DesiredPlayersController _desiredPlayersController;
        protected readonly ManagementController _managementController;
        
        protected readonly UserInfoBL _user;
        protected readonly ILogger<UserController> _logger;
        
        public UserController(UserInfoBL user, ILogger<UserController> logger, 
            FunctionController functionController, PlayerStatisticsController playerStatisticsController,
            PlayerController playerController, TeamController teamController, 
            DesiredPlayersController desiredPlayersController, ManagementController managementController)
        {
            _functionController = functionController;
            _playerStatisticsController = playerStatisticsController;

            _playerController = playerController;
            _teamController = teamController;
            _desiredPlayersController = desiredPlayersController;
            _managementController = managementController;
            
            _user = user;
            _logger = logger;
        }
        
        public List<PlayerBL> GetAllPlayers()
        {
            return _playerController.GetAll();
        }
        
        public List<PlayersTeamStatBL> GetPlayerTeamStat()
        {
            return _functionController.GetPlayersTeamStat();
        }
        
        public List<PlayerBL> GetPlayersByTeam(int teamID)
        {
            TeamBL team = _teamController.FindTeamById(teamID);
            if (team == null)
            {
                return null;
            }
            return _playerController.GetPlayersByTeam(team);
        }
        
        public PlayerBL FindPlayerByID(int id)
        {
            return _playerController.FindPlayerById(id);
        }
        
        public PlayerBL FindPlayerByName(string name)
        {
            return _playerController.FindPlayerByName(name);
        }
        
        public List<TeamBL> GetAllTeams()
        {
            return _teamController.GetAll();
        }
        
        public TeamBL FindTeamByID(int id)
        {
            return _teamController.FindTeamById(id);
        }
        
        public TeamBL FindTeamByName(string name)
        {
            return _teamController.FindTeamByName(name);
        }
        
        public PlayerstatisticBL GetPlayerStatistic(int id)
        {
            return _playerStatisticsController.GetPlayerStatisticById(id);
        }
    }
}
