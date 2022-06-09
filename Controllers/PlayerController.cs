using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class PlayerController
    {
        private readonly IPlayerRepository _playerRepository;
        
        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool Add(PlayerBL player)
        {
            return _playerRepository.Add(player);
        }

        public List<PlayerBL> GetAll()
        {
            var players = _playerRepository.GetAll();
            return players.Any() ? players.ToList() : null;
        }

        public bool Update(PlayerBL player)
        {
            return _playerRepository.Update(player);
        }
        
        public StatusCode Delete(int id)
        {
            return _playerRepository.Delete(id);
        }

        public List<PlayerBL> GetPlayersByTeam(TeamBL team)
        {
            var players = _playerRepository.GetPlayersByTeam(team);
            return players.Any() ? players.ToList() : null;
        }

        public PlayerBL FindPlayerById(int id)
        {
            return _playerRepository.FindPlayerById(id);
        }

        public PlayerBL FindPlayerByName(string name)
        {
            var player = _playerRepository.FindPlayerByName(name);
            return player.Any() ? player.First() : null;
        }
    }
}