using Models.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Builders
{
    class DesiredPlayerBuilder
    {
        private int id;
        private int playerId;
        private int managementId;

        public DesiredPlayerBuilder()
        {
            id = 0;
            playerId = 0;
            managementId = 0;
        }

        public DesiredPlayerBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public DesiredPlayerBuilder WithPlayerId(int playerId)
        {
            this.playerId = playerId;
            return this;
        }

        public DesiredPlayerBuilder WithManagementId(int managementId)
        {
            this.managementId = managementId;
            return this;
        }

        public DesiredplayerBL Build()
        {
            return new DesiredplayerBL()
            {
                Id = this.id,
                PlayerId = this.playerId,
                Managementid = this.managementId
            };
        }
    }
}
