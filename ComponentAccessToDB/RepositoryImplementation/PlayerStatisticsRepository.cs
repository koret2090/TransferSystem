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
    public class PlayerStatisticsRepository : IPlayerStatisticsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<PlayerStatisticsRepository> _logger;
        
        public PlayerStatisticsRepository(transfersystemContext curDb, ILogger<PlayerStatisticsRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(PlayerstatisticBL element)
        {
            try
            {
                var elem = new Playerstatistic(element);
                elem.Statisticsid = db.Playerstatistics.Count() + 1;
                db.Playerstatistics.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Statistics {Number} added at {dateTime}", elem.Statisticsid, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<PlayerstatisticBL> GetAll()
        {
            return ConvertToBL(db.Playerstatistics);
        }
        
        public bool Update(PlayerstatisticBL element)
        {
            try
            {
                var elem = new Playerstatistic(element);
                db.Playerstatistics.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Statistics {Number} updated at {dateTime}", elem.Statisticsid, DateTime.UtcNow);
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
                var elem = db.Playerstatistics.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Playerstatistics.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Statistics {Number} removed at {dateTime}", elem.Statisticsid, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public PlayerstatisticBL GetPlayerStatisticById(int id)
        {
            var res = db.Playerstatistics.Find(id);
            if (res != null) 
                return new PlayerstatisticBL(res);
            return null;
        }
        
        public List<PlayerstatisticBL> GetStatisticsByPlayer(PlayerBL element)
        {
            var elem = new Player(element);
            return ConvertToBL(db.Playerstatistics.Where(needed => needed.Players.Contains(elem)));
        }
        
        private List<PlayerstatisticBL> ConvertToBL(IQueryable<Playerstatistic> args)
        {
            List<PlayerstatisticBL> elements = new List<PlayerstatisticBL>();
            foreach (var elem in args)
            {
                elements.Add(new PlayerstatisticBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
