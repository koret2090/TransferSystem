using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB;
using Microsoft.Extensions.Logging;
using ComponentAccessToDB.RepositoryImplementation;
using ComponentAccessToDB.RepositoryInterfaces;
using Controllers;
using Models;
using Models.ModelsBL;

namespace ComponentBuisinessLogic
{
    public class ManagerController : UserController
    {
        private readonly AvailableDealsController _availableDealsController;
        
        public ManagerController(UserInfoBL user, ILogger<UserController> logger, 
            FunctionController functionController, PlayerStatisticsController playerStatisticsController,
            PlayerController playerController, TeamController teamController,
            DesiredPlayersController desiredPlayersController, ManagementController managementController, 
            AvailableDealsController availableDealsController) : 
            base(user, logger, functionController, playerStatisticsController, playerController, 
                teamController, desiredPlayersController, managementController)
        {
            _availableDealsController = availableDealsController;
        }

        public List<DesiredplayerBL> GetAllDesiredPlayers()
        {
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management != null)
            {
                return _desiredPlayersController.GetPlayersByManagement(management);
            }
            return null;
        }
        
        public bool RequestPlayer(int playerID, int cost)
        {
            PlayerBL player = _playerController.FindPlayerById(playerID);
            if (player == null)
            {
                return false;
            }
            TeamBL team = _teamController.FindTeamByPlayer(player);
            if (team == null)
            {
                return false;
            }
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management == null)
            {
                return false;
            }
            AvailabledealBL deal = new AvailabledealBL () { PlayerId = playerID, TomanagementId = team.ManagementId, FrommanagementId = management.ManagementId, Cost = cost, Status = (int)Status.NotSeen };
            return _availableDealsController.Add(deal);
        }
        
        public bool ConfirmDeal(int dealID)
        {
            AvailabledealBL deal = _availableDealsController.GetDealById(dealID);
            if (deal == null)
            {
                return false;
            }
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management == null || deal.TomanagementId != management.ManagementId)
            {
                return false;
            }
            TeamBL team = _teamController.FindTeamByManagement((int)management.ManagementId);
            if (team == null)
            {
                return false;
            }
            if (deal.Cost > team.Balance)
            {
                return false;
            }
            _availableDealsController.ConfirmDeal(deal);
            return true;
        }
        
        public bool RejectDeal(int dealID)
        {
            AvailabledealBL deal = _availableDealsController.GetDealById(dealID);
            if (deal == null)
            {
                return false;
            }
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management == null || deal.TomanagementId != management.ManagementId)
            {
                return false;
            }
            _availableDealsController.RejectDeal(deal);
            return true;
        }
        
        public List<AvailabledealBL> GetIncomingDeals()
        {
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management == null)
            {
                return null;
            }
            return _availableDealsController.GetIncomingDeals(management);
        }
        
        public List<AvailabledealBL> GetOutgoingDeals()
        {
            ManagementBL management = _managementController.FindByManager(_user.Id);
            if (management == null)
            {
                return null;
            }
            return _availableDealsController.GetOutgoingDeals(management);
        }
        
        public StatusCode DeleteDesiredPlayer(int id)
        {
            return _desiredPlayersController.Delete(id);
        }
    }
}
