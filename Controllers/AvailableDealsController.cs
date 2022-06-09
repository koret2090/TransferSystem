using ComponentAccessToDB.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class AvailableDealsController
    {
        private readonly IAvailableDealsRepository _availableDealsRepository;

        public AvailableDealsController(IAvailableDealsRepository availableDealsRepository)
        {
            _availableDealsRepository = availableDealsRepository;
        }

        public bool Add(AvailabledealBL availabledeal)
        {
            return _availableDealsRepository.Add(availabledeal);
        }

        public List<AvailabledealBL> GetAll()
        {
            var availabledeals = _availableDealsRepository.GetAll();
            return availabledeals.Any() ? availabledeals.ToList() : null;
        }

        public bool Update(AvailabledealBL availabledeal)
        {
            return _availableDealsRepository.Update(availabledeal);
        }

        public StatusCode Delete(int id)
        {
            return _availableDealsRepository.Delete(id);
        }

        public AvailabledealBL GetDealById(int id)
        {
            return _availableDealsRepository.GetDealById(id);
        }

        public bool ConfirmDeal(AvailabledealBL availabledeal)
        {
            return _availableDealsRepository.ConfirmDeal(availabledeal);
        }

        public bool RejectDeal(AvailabledealBL availabledeal)
        {
            return _availableDealsRepository.RejectDeal(availabledeal);
        }

        public List<AvailabledealBL> GetIncomingDeals(ManagementBL management)
        {
            var availabledeals = _availableDealsRepository.GetIncomingDeals(management);
            return availabledeals.Any() ? availabledeals.ToList() : null;
        }

        public List<AvailabledealBL> GetOutgoingDeals(ManagementBL management)
        {
            var availabledeals = _availableDealsRepository.GetOutgoingDeals(management);
            return availabledeals.Any() ? availabledeals.ToList() : null;
        }
    }
}
