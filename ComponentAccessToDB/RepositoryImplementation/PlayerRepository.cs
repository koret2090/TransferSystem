using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;
using Models.ModelsDB;

namespace ComponentAccessToDB.RepositoryImplementation
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<PlayerRepository> _logger;
        
        public PlayerRepository(transfersystemContext curDb, ILogger<PlayerRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(PlayerBL element)
        {
            try
            {
                var elem = new Player(element);
                elem.Playerid = db.Players.Count() + 1;
                db.Players.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} added at {dateTime}", element.Name, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<PlayerBL> GetAll()
        {
            return ConvertToBL(db.Players);
        }
        
        public bool Update(PlayerBL element)
        {
            try
            {
                var elem = new Player(element);
                db.Players.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} updated at {dateTime}", element.Name, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public StatusCode Delete(int id)
        {
            try
            {
                var elem = db.Players.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Players.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} removed at {dateTime}", elem.Name, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public List<PlayerBL> GetPlayersByTeam(TeamBL element)
        {
            return ConvertToBL(db.Players.Where(needed => needed.Team.Name == element.Name));
        }
        
        public PlayerBL FindPlayerById(int id)
        {
            var res = db.Players.Find(id);
            if (res != null) 
                return new PlayerBL(res);
            return null;
        }
        
        public List<PlayerBL> FindPlayerByName(string name)
        {
            return ConvertToBL(db.Players.Where(needed => needed.Name == name));
        }
        
        private List<PlayerBL> ConvertToBL(IQueryable<Player> args)
        {
            List<PlayerBL> elements = new List<PlayerBL>();
            foreach (var elem in args)
            {
                elements.Add(new PlayerBL(elem));
            }

            if (elements.Count == 0)
                return null;
            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
