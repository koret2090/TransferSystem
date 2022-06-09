using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class PlayerStatisticsController
    {
        private readonly IPlayerStatisticsRepository _playerStatisticsRepository;
        
        public PlayerStatisticsController(IPlayerStatisticsRepository playerStatisticsRepository)
        {
            _playerStatisticsRepository = playerStatisticsRepository;
        }

        public bool Add(PlayerstatisticBL playerstatistic)
        {
            return _playerStatisticsRepository.Add(playerstatistic);
        }

        public List<PlayerstatisticBL> GetAll()
        {
            var playerstatistics = _playerStatisticsRepository.GetAll();
            return playerstatistics.Any() ? playerstatistics.ToList() : null;
        }
        
        public bool Update(PlayerstatisticBL playerstatistic)
        {
            return _playerStatisticsRepository.Update(playerstatistic);
        }
        
        public StatusCode Delete(int id)
        {
            return _playerStatisticsRepository.Delete(id);
        }

        public PlayerstatisticBL GetPlayerStatisticById(int id)
        {
            return _playerStatisticsRepository.GetPlayerStatisticById(id);
        }

        public PlayerstatisticBL GetStatisticsByPlayer(PlayerBL player)
        {
            var playerstatistics = _playerStatisticsRepository.GetStatisticsByPlayer(player);
            return playerstatistics.Any() ? playerstatistics.First() : null;
        }
    }
}