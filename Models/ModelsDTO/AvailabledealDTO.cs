using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class AvailabledealDTO
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? TomanagementId { get; set; }
        public int? FrommanagementId { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }

        public AvailabledealDTO()
        {
            
        }

        public AvailabledealDTO(AvailabledealBL availabledeal)
        {
            Id = availabledeal.Id;
            PlayerId = availabledeal.PlayerId;
            TomanagementId = availabledeal.TomanagementId;
            FrommanagementId = availabledeal.FrommanagementId;
            Cost = availabledeal.Cost;
            Status = availabledeal.Status;
        }       
    }
}
