using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class ManagementBL
    {        
        public int ManagementId { get; set; }
        public int? AnalysistId { get; set; }
        public int? ManagerId { get; set; }
        
        public ManagementBL(Management management)
        {
            ManagementId = management.Managementid;
            AnalysistId = management.Analysistid;
            ManagerId = management.Managerid;
        }
        
        public ManagementBL()
        {
            
        }
    }
}
