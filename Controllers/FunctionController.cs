using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB.RepositoryInterfaces;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Controllers
{
    public class FunctionController
    {
        private readonly IFunctionsRepository _functionsRepository;

        public FunctionController(IFunctionsRepository functionsRepository)
        {
            _functionsRepository = functionsRepository;
        }

        public List<PlayersTeamStatBL> GetPlayersTeamStat()
        {
            var players = _functionsRepository.GetPlayersTeamStat();
            return players.Any() ? players.ToList() : null;
        }
    }
}
