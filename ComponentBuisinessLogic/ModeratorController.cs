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
    public enum DealResult
    {
        ElementNotFound,
        BadRequest,
        Ok
    }
    
    public class ModeratorController : UserController
    {
        private readonly AvailableDealsController _availableDealsController;
        private readonly UserInfoController _userInfoController;
        
        public ModeratorController(UserInfoBL user, ILogger<UserController> logger, 
            FunctionController functionController, PlayerStatisticsController playerStatisticsController,
            PlayerController playerController, TeamController teamController,
            DesiredPlayersController desiredPlayersController, ManagementController managementController, 
            AvailableDealsController availableDealsController, UserInfoController userInfoController) : 
            base(user, logger, functionController, playerStatisticsController, playerController, 
                teamController, desiredPlayersController, managementController)
        {
            _availableDealsController = availableDealsController;
            _userInfoController = userInfoController;
        }

        public DealResult MakeDeal(int dealID)
        {
            AvailabledealBL deal = _availableDealsController.GetDealById(dealID);
            if (deal == null)
            {
                _logger.LogError("Deal {Number} was not fount at {dateTime}", dealID, DateTime.UtcNow);
                return DealResult.ElementNotFound;
            }
            TeamBL newTeam = _teamController.FindTeamByManagement((int)deal.FrommanagementId);
            if (newTeam == null)
            {
                _logger.LogError("New team was not fount by Tomanagementid {id} at {dateTime}", (int)deal.TomanagementId, DateTime.UtcNow);
                return DealResult.BadRequest;
            }
            TeamBL lastTeam = _teamController.FindTeamByManagement((int)deal.TomanagementId);
            if (lastTeam == null)
            {
                _logger.LogError("Last team was not fount by Frommanagementid {id} at {dateTime}", (int)deal.FrommanagementId, DateTime.UtcNow);
                return DealResult.BadRequest;
            }
            PlayerBL player = _playerController.FindPlayerById((int)deal.PlayerId);
            if (player == null)
            {
                _logger.LogError("Player {Number} was not fount at {dateTime}", (int)deal.PlayerId, DateTime.UtcNow);
                return DealResult.BadRequest;
            }
            if (! CheckOportunityToBuy(deal.Cost, newTeam))
            {
                _logger.LogError("Deal cost {Number} is more than team balance at {dateTime}", deal.Cost, DateTime.UtcNow);
                return DealResult.BadRequest;
            }
            UpdateTeamBalance(lastTeam, newTeam, deal.Cost);
            UpdatePlayerTeam(player, newTeam.TeamId);
            _availableDealsController.Delete(dealID);
            return DealResult.Ok;
        }
        
        public StatusCode DeleteDeal(int dealID)
        {
            return _availableDealsController.Delete(dealID);
        }
        
        public bool UpdatePlayerTeam(PlayerBL player, int team)
        {
            player.TeamId = team;
            return _playerController.Update(player);
        }
        
        private bool UpdateTeamBalance(TeamBL lastTeam, TeamBL newTeam, int cost)
        {
            lastTeam.Balance += cost;
            newTeam.Balance -= cost;
            if (!_teamController.Update(lastTeam))
            {
                return false;
            }
            return _teamController.Update(newTeam);
        }
        
        private bool CheckOportunityToBuy(int cost, TeamBL team)
        {
            return cost < team.Balance;
        }
        
        public List<AvailabledealBL> GetAllDeals()
        {
            return _availableDealsController.GetAll();
        }
        
        public bool AddNewUser(UserInfoBL user)
        {
            if (_userInfoController.FindUserByLogin(user.Login) != null)
            {
                return false;
            }
            _userInfoController.Add(user);
            return true;
        }
        
        public StatusCode DeleteUser(int id)
        {
            return _userInfoController.Delete(id);
        }
        
        public List<UserInfoBL> GetAllUsers()
        {
            return _userInfoController.GetAll();
        }
    }
}
