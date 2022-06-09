using Models.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    class ManagementBuilder
    {
        private int managementId;
        private int analysistId;
        private int managerId;

        public ManagementBuilder()
        {
            managementId = 0;
            analysistId = 0;
            managerId = 0;
        }

        public ManagementBuilder WithManagementId(int managementId)
        {
            this.managementId = managementId;
            return this;
        }

        public ManagementBuilder WithAnalysistId(int analysistId)
        {
            this.analysistId = analysistId;
            return this;
        }

        public ManagementBuilder WithManagerId(int managerId)
        {
            this.managerId = managerId;
            return this;
        }

        public ManagementBL Build()
        {
            return new ManagementBL()
            {
                ManagementId = this.managementId,
                AnalysistId = this.analysistId,
                ManagerId = this.managerId
            };
        }
    }
}
