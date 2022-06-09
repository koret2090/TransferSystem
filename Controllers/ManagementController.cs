using System.Collections.Generic;
using System.Linq;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Controllers
{
    public class ManagementController
    {
        private readonly IManagementRepository _managementRepository;
        
        public ManagementController(IManagementRepository managementRepository)
        {
            _managementRepository = managementRepository;
        }

        public bool Add(ManagementBL management)
        {
            return _managementRepository.Add(management);
        }

        public List<ManagementBL> GetAll()
        {
            var managements = _managementRepository.GetAll();
            return managements.Any() ? managements.ToList() : null;
        }
        
        public bool Update(ManagementBL management)
        {
            return _managementRepository.Update(management);
        }
        
        public StatusCode Delete(int id)
        {
            return _managementRepository.Delete(id);
        }

        public ManagementBL FindByAnalytic(int id)
        {
            var managements = _managementRepository.FindByAnalytic(id);
            return managements.Any() ? managements.First() : null;
        }
        
        public ManagementBL FindByManager(int id)
        {
            var managements = _managementRepository.FindByManager(id);
            return managements.Any() ? managements.First() : null;
        }

        public ManagementBL FindManagementById(int id)
        {
            return _managementRepository.FindManagementById(id);
        }
    }
}