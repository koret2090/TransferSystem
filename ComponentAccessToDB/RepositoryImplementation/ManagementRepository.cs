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
    public class ManagementRepository : IManagementRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<ManagementRepository> _logger;
        
        public ManagementRepository(transfersystemContext curDb, ILogger<ManagementRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        
        public bool Add(ManagementBL element)
        {
            try
            {
                var elem = new Management(element);
                elem.Managementid = db.Managements.Count() + 1;
                db.Managements.Add(elem);
                db.SaveChanges();
                _logger.LogInformation("Management {Number} added at {dateTime}", elem.Managementid, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<ManagementBL> GetAll()
        {
            return ConvertToBL(db.Managements);
        }
        
        public bool Update(ManagementBL element)
        {
            try
            {
                var elem = new Management(element);
                db.Managements.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Management {Number} updated at {dateTime}", elem.Managementid, DateTime.UtcNow);
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
                var elem = db.Managements.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Managements.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Management {Number} deleted at {dateTime}", elem.Managementid, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public List<ManagementBL> FindByAnalytic(int id)
        {
            return ConvertToBL(db.Managements.Where(needed => needed.Analysistid == id));
        }
        
        public List<ManagementBL> FindByManager(int id)
        {
            return ConvertToBL(db.Managements.Where(needed => needed.Managerid == id));
        }

        public ManagementBL FindManagementById(int id)
        {
            var res = db.Managements.Find(id);
            if (res != null) 
                return new ManagementBL(res);
            return null;
        }
        
        private List<ManagementBL> ConvertToBL(IQueryable<Management> args)
        {
            List<ManagementBL> elements = new List<ManagementBL>();
            foreach (var elem in args)
            {
                elements.Add(new ManagementBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
