using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Availabledeal
    {
        public int Id { get; set; }
        public int? Playerid { get; set; }
        public int? Tomanagementid { get; set; }
        public int? Frommanagementid { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }

        public virtual Management Frommanagement { get; set; }
        public virtual Player Player { get; set; }
        public virtual Management Tomanagement { get; set; }

        public Availabledeal(AvailabledealBL deal)
        {
            Id = deal.Id;
            Playerid = deal.PlayerId;
            Tomanagementid = deal.TomanagementId;
            Frommanagementid = deal.FrommanagementId;
            Cost = deal.Cost;
            Status = deal.Status;
        }

        public Availabledeal()
        {
            
        }
    }
}
