using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB.RepositoryInterfaces;
using Models.ModelsBL;
using Models.ModelsDB;

namespace ComponentAccessToDB.RepositoryImplementation
{
    public class FunctionRepository : IFunctionsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<FunctionRepository> _logger;
        
        public FunctionRepository(transfersystemContext curDb, ILogger<FunctionRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public List<PlayersTeamStatBL> GetPlayersTeamStat()
        {
            return ConvertToBL(db.getplayers());
        }
        
        private List<PlayersTeamStatBL> ConvertToBL(IQueryable<PlayersTeamStat> args)
        {
            List<PlayersTeamStatBL> elements = new List<PlayersTeamStatBL>();
            foreach (var elem in args)
            {
                elements.Add(new PlayersTeamStatBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
