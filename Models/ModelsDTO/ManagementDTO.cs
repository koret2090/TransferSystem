using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class ManagementDTO
    {        
        public int ManagementId { get; set; }
        public int? AnalysistId { get; set; }
        public int? ManagerId { get; set; }

        public ManagementDTO()
        {
            
        }

        public ManagementDTO(ManagementBL management)
        {
            ManagementId = management.ManagementId;
            AnalysistId = management.AnalysistId;
            ManagerId = management.ManagementId;
        }
    }
}
