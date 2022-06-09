using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class AvailabledealBL
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? TomanagementId { get; set; }
        public int? FrommanagementId { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }

        public AvailabledealBL()
        {
            
        }
        
        public AvailabledealBL(Availabledeal deal)
        {
            Id = deal.Id;
            PlayerId = deal.Playerid;
            TomanagementId = deal.Tomanagementid;
            FrommanagementId = deal.Frommanagementid;
            Cost = deal.Cost;
            Status = deal.Status;
        }
    }
}
