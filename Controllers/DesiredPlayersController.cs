using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class DesiredPlayersController
    {
        private readonly IDesiredPlayersRepository _desiredPlayersRepository;

        public DesiredPlayersController(IDesiredPlayersRepository desiredPlayersRepository)
        {
            _desiredPlayersRepository = desiredPlayersRepository;
        }

        public bool Add(DesiredplayerBL desiredplayer)
        {
            return _desiredPlayersRepository.Add(desiredplayer);
        }

        public List<DesiredplayerBL> GetAll()
        {
            var desiredplayers = _desiredPlayersRepository.GetAll();
            return desiredplayers.Any() ? desiredplayers.ToList() : null;
        }

        public bool Update(DesiredplayerBL desiredplayer)
        {
            return _desiredPlayersRepository.Update(desiredplayer);
        }

        public StatusCode Delete(int id)
        {
            return _desiredPlayersRepository.Delete(id);
        }

        public DesiredplayerBL GetPlayerById(int id)
        {
            return _desiredPlayersRepository.GetPlayerById(id);
        }

        public List<DesiredplayerBL> GetPlayersByManagement(ManagementBL management)
        {
            var players = _desiredPlayersRepository.GetPlayersByManagement(management);
            return players.Any() ? players.ToList() : null;
        }
    }
}
