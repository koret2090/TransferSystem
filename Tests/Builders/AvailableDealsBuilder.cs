using Models.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    class AvailableDealsBuilder
    {
        private int id;
        private int playerId;
        private int toManagementId;
        private int fromManagementId;
        private int cost;
        private int status;

        public AvailableDealsBuilder()
        {
            id = 0;
            playerId = 0;
            toManagementId = 0;
            fromManagementId = 0;
            cost = 0;
            status = 0;
        }

        public AvailableDealsBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public AvailableDealsBuilder WithPlayerId(int playerId)
        {
            this.playerId = playerId;
            return this;
        }

        public AvailableDealsBuilder WithToManagmentId(int toManagementId)
        {
            this.toManagementId = toManagementId;
            return this;
        }

        public AvailableDealsBuilder WithFromManagmentId(int fromManagementId)
        {
            this.fromManagementId = fromManagementId;
            return this;
        }

        public AvailableDealsBuilder WithCost(int cost)
        {
            this.cost = cost;
            return this;
        }

        public AvailableDealsBuilder WithStatus(int status)
        {
            this.status = status;
            return this;
        }

        public AvailabledealBL Build()
        {
            return new AvailabledealBL()
            {
                Id = this.id,
                PlayerId = this.playerId,
                TomanagementId = this.toManagementId,
                FrommanagementId = this.fromManagementId,
                Cost = this.cost,
                Status = this.status
            };
        }
    }
}
