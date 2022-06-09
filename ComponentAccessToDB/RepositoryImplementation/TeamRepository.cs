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
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<TeamRepository> _logger;
        public TeamRepository(transfersystemContext curDb, ILogger<TeamRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(TeamBL element)
        {
            try
            {
                var elem = new Team(element);
                elem.Teamid = db.Teams.Count() + 1;
                db.Teams.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} added at {dateTime}", elem.Name, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<TeamBL> GetAll()
        {
            return ConvertToBL(db.Teams);
        }
        
        public bool Update(TeamBL element)
        {
            try
            {
                var elem = new Team(element);
                db.Teams.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} updated at {dateTime}", element.Name, DateTime.UtcNow);
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
                var elem = db.Teams.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Teams.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} removed at {dateTime}", elem.Name, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public TeamBL FindTeamById(int id)
        {
            var res = db.Teams.Find(id);
            if (res != null) 
                return new TeamBL(res);
            return null;
        }
        
        public List<TeamBL> FindTeamByName(string name)
        {
            return ConvertToBL(db.Teams.Where(needed => needed.Name == name));
        }
        
        public List<TeamBL> FindTeamByPlayer(PlayerBL player)
        {
            var elem = new Player(player);
            return ConvertToBL(db.Teams.Where(needed => needed.Players.Contains(elem)));
        }
        
        public List<TeamBL> FindTeamByManagement(int managementID)
        {
            return ConvertToBL(db.Teams.Where(needed => needed.Managementid == managementID));
        }
        
        private List<TeamBL> ConvertToBL(IQueryable<Team> args)
        {
            List<TeamBL> elements = new List<TeamBL>();
            foreach (var elem in args)
            {
                elements.Add(new TeamBL(elem));
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
