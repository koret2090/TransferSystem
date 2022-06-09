using System;
using System.Collections.Generic;
using System.Linq;
using ComponentAccessToDB.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Models;
using Models.ModelsBL;
using Models.ModelsDB;

namespace ComponentAccessToDB.RepositoryImplementation
{
    public class TeamStatisticsRepository : ITeamStatisticsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<TeamStatisticsRepository> _logger;
        
        public TeamStatisticsRepository(transfersystemContext curDb, ILogger<TeamStatisticsRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(TeamstatisticBL element)
        {
            try
            {
                var elem = new Teamstatistic(element);
                elem.Statisticsid = db.Teamstatistics.Count() + 1;
                db.Teamstatistics.Add(elem);
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
        
        public List<TeamstatisticBL> GetAll()
        {
            return ConvertToBL(db.Teamstatistics);
        }
        
        public bool Update(TeamstatisticBL element)
        {
            try
            {
                var elem = new Teamstatistic(element);
                db.Teamstatistics.Update(elem);
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
                var elem = db.Teamstatistics.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Teamstatistics.Remove(elem);
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
        
        public TeamstatisticBL GetTeamStatisticById(int id)
        {
            var res = db.Teamstatistics.Find(id);
            if (res != null) 
                return new TeamstatisticBL(res);
            return null;
        }
        
        public List<TeamstatisticBL> GetStatisticsByTeam(TeamBL element)
        {
            var elem = new Team(element);
            return ConvertToBL(db.Teamstatistics.Where(needed => needed.Teams.Contains(elem)));
        }
        
        private List<TeamstatisticBL> ConvertToBL(IQueryable<Teamstatistic> args)
        {
            List<TeamstatisticBL> elements = new List<TeamstatisticBL>();
            foreach (var elem in args)
            {
                elements.Add(new TeamstatisticBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}