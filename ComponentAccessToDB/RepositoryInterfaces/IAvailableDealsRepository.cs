using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IAvailableDealsRepository : ICrudRepository<AvailabledealBL>
    {
        AvailabledealBL GetDealById(int id);
        bool ConfirmDeal(AvailabledealBL element);
        bool RejectDeal(AvailabledealBL element);
        List<AvailabledealBL> GetIncomingDeals(ManagementBL element);
        List<AvailabledealBL> GetOutgoingDeals(ManagementBL element);
    }
}
