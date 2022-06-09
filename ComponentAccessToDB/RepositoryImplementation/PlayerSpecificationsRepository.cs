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
    public class PlayerSpecificationsRepository : IPlayerSpecificationsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<PlayerSpecificationsRepository> _logger;
        
        public PlayerSpecificationsRepository(transfersystemContext curDb, ILogger<PlayerSpecificationsRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(PlayerspecificationBL element)
        {
            try
            {
                var elem = new Playerspecification(element);
                elem.Specificationsid = db.Playerspecifications.Count() + 1;
                db.Playerspecifications.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Specification {Number} added at {dateTime}", elem.Specificationsid, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<PlayerspecificationBL> GetAll()
        {
            return ConvertToBL(db.Playerspecifications);
        }
        
        public bool Update(PlayerspecificationBL element)
        {
            try
            {
                var elem = new Playerspecification(element);
                db.Playerspecifications.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Specification {Number} updated at {dateTime}", elem.Specificationsid, DateTime.UtcNow);
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
                var elem = db.Playerspecifications.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Playerspecifications.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Statistics {Number} removed at {dateTime}", elem.Specificationsid, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public PlayerspecificationBL GetPlayerSpecificationById(int id)
        {
            var res = db.Playerspecifications.Find(id);
            if (res != null) 
                return new PlayerspecificationBL(res);
            return null;
        }
        
        public List<PlayerspecificationBL> GetSpecificationByPlayer(PlayerBL element)
        {
            var elem = new Player(element);
            return ConvertToBL(db.Playerspecifications.Where(needed => needed.Players.Contains(elem)));
        }
        
        private List<PlayerspecificationBL> ConvertToBL(IQueryable<Playerspecification> args)
        {
            List<PlayerspecificationBL> elements = new List<PlayerspecificationBL>();
            foreach (var elem in args)
            {
                elements.Add(new PlayerspecificationBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}