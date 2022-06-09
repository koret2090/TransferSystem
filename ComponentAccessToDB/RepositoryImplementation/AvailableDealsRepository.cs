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
    public enum Status
    {
        Confirmed = 0,
        Rejected = 1,
        NotSeen = 2
    }
    
    public class AvailableDealsRepository : IAvailableDealsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<AvailableDealsRepository> _logger;

        public AvailableDealsRepository(transfersystemContext curDb, ILogger<AvailableDealsRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }

        public bool Add(AvailabledealBL element)
        {
            try
            {
                var deal = new Availabledeal(element);
                deal.Id = db.Availabledeals.Count() + 1;
                db.Availabledeals.Add(deal);
                db.SaveChanges();
                _logger.LogInformation("Deal {Number} added at {dateTime}", element.Id, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<AvailabledealBL> GetAll()
        {
            return ConvertToBL(db.Availabledeals);
        }
        
        public bool Update(AvailabledealBL element)
        {
            try
            {
                var deal = new Availabledeal(element);
                db.Availabledeals.Update(deal);
                db.SaveChanges(); 
                _logger.LogInformation("Deal {Number} updated at {dateTime}", element.Id, DateTime.UtcNow);
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
                var elem = db.Availabledeals.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Availabledeals.Remove(elem);
                db.SaveChanges();
                _logger.LogInformation("Deal {Number} removed at {dateTime}", id, DateTime.UtcNow);
                return StatusCode.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode.BadRequest;
            }
        }
        
        public AvailabledealBL GetDealById(int id)
        {
            var res = db.Availabledeals.Find(id);
            if (res != null) 
                return new AvailabledealBL(res);
            return null;
        }
        
        public bool ConfirmDeal(AvailabledealBL element)
        {
            try
            {
                var elem = new Availabledeal(element);
                elem.Status = (int)Status.Confirmed;
                db.Availabledeals.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Deal {Number} confirmed at {dateTime}", element.Id, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public bool RejectDeal(AvailabledealBL element)
        {
            try
            {
                var elem = new Availabledeal(element);
                elem.Status = (int)Status.Rejected;
                db.Availabledeals.Update(elem);
                db.SaveChanges();
                _logger.LogInformation("Deal {Number} rejected at {dateTime}", element.Id, DateTime.UtcNow);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        
        public List<AvailabledealBL> GetIncomingDeals(ManagementBL element)
        {
            if (element == null)
            {
                return null;
            }

            var elem = new Management(element);
            return ConvertToBL(db.Availabledeals.Where(needed => needed.Tomanagementid == elem.Managementid));
        }
        
        public List<AvailabledealBL> GetOutgoingDeals(ManagementBL element)
        {
            if (element == null)
            {
                return null;
            }
            var elem = new Management(element);
            return ConvertToBL(db.Availabledeals.Where(needed => needed.Frommanagementid == elem.Managementid));
        }
        
        private List<AvailabledealBL> ConvertToBL(IQueryable<Availabledeal> args)
        {
            List<AvailabledealBL> elements = new List<AvailabledealBL>();
            foreach (var elem in args)
            {
                elements.Add(new AvailabledealBL(elem));
            }

            return elements;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
