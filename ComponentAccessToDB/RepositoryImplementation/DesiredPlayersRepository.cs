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
    public class DesiredPlayersRepository : IDesiredPlayersRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<DesiredPlayersRepository> _logger;
        
        public DesiredPlayersRepository(transfersystemContext curDb, ILogger<DesiredPlayersRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(DesiredplayerBL element)
        {
            try
            {
                var elem = new Desiredplayer(element);
                elem.Id = db.Desiredplayers.Count() + 1;
                db.Desiredplayers.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} added at {dateTime}", elem.Playerid, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<DesiredplayerBL> GetAll()
        {
            return ConvertToBL(db.Desiredplayers);
        }
        
        public bool Update(DesiredplayerBL element)
        {
            try
            {
                var elem = new Desiredplayer(element);
                db.Desiredplayers.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} updated at {dateTime}", elem.Playerid, DateTime.UtcNow);
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
                var elem = db.Desiredplayers.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Desiredplayers.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} deleted at {dateTime}", elem.Playerid, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public DesiredplayerBL GetPlayerById(int id)
        {
            var res = db.Desiredplayers.Find(id);
            if (res != null) 
                return new DesiredplayerBL(res);
            return null;
        }

        public List<DesiredplayerBL> GetPlayersByManagement(ManagementBL element)
        {
            var management = new Management(element);
            return ConvertToBL(db.Desiredplayers.Where(needed =>
                needed.Player.Team.Managementid == management.Managementid
            ));
        }
        
        private List<DesiredplayerBL> ConvertToBL(IQueryable<Desiredplayer> args)
        {
            List<DesiredplayerBL> elements = new List<DesiredplayerBL>();
            foreach (var elem in args)
            {
                elements.Add(new DesiredplayerBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
