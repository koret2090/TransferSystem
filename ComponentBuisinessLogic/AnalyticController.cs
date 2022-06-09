using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB;
using Microsoft.Extensions.Logging;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models;
using Models.ModelsBL;

namespace ComponentBuisinessLogic
{
    public class AnalyticController : UserController
    {
        public AnalyticController(UserInfoBL user, ILogger<UserController> logger, 
            FunctionController functionController, PlayerStatisticsController playerStatisticsController,
            PlayerController playerController, TeamController teamController, 
            DesiredPlayersController desiredPlayersController, ManagementController managementController) : 
            base(user, logger, functionController, playerStatisticsController, playerController, 
                teamController, desiredPlayersController, managementController)
        {
        }
        
        public List<DesiredplayerBL> GetAllDesiredPlayers()
        {
            ManagementBL management = _managementController.FindByAnalytic(_user.Id);
            if (management == null)
            {
                return null;
            }
            return _desiredPlayersController.GetPlayersByManagement(management);
        }
        
        public bool AddDesiredPlayer(int playerId)
        {
            PlayerBL player = _playerController.FindPlayerById(playerId);
            if (player == null)
            {
                return false;
            }
            ManagementBL management = _managementController.FindByAnalytic(_user.Id);
            if (management == null)
            {
                return false;
            }
            DesiredplayerBL newDesirePlayer = new DesiredplayerBL () { PlayerId = player.PlayerId};
            return _desiredPlayersController.Add(newDesirePlayer);
        }
        
        public StatusCode DeleteDesiredPlayer(int id)
        {
            return _desiredPlayersController.Delete(id);
        }
    }
}
