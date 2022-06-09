using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class PlayerSpecificationsController
    {
        private readonly IPlayerSpecificationsRepository _playerSpecificationsRepository;

        public PlayerSpecificationsController(IPlayerSpecificationsRepository playerSpecificationsRepository)
        {
            _playerSpecificationsRepository = playerSpecificationsRepository;
        }

        public bool Add(PlayerspecificationBL playerspecification)
        {
            return _playerSpecificationsRepository.Add(playerspecification);
        }

        public List<PlayerspecificationBL> GetAll()
        {
            var  playerspecifications = _playerSpecificationsRepository.GetAll();
            return playerspecifications.Any() ? playerspecifications.ToList() : null;
        }

        public bool Update(PlayerspecificationBL playerspecification)
        {
            return _playerSpecificationsRepository.Update(playerspecification);
        }
        
        public StatusCode Delete(int id)
        {
            return _playerSpecificationsRepository.Delete(id);
        }

        public PlayerspecificationBL GetPlayerSpecificationById(int id)
        {
            return _playerSpecificationsRepository.GetPlayerSpecificationById(id);
        }

        public PlayerspecificationBL GetSpecificationByPlayer(PlayerBL player)
        {
            var playerspecifications = _playerSpecificationsRepository.GetSpecificationByPlayer(player);
            return playerspecifications.Any() ? playerspecifications.First() : null;
        }
    }
}