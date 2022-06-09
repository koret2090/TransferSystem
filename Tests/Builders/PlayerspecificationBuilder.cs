using Models.ModelsBL;

namespace Tests.Builders
{
    public class PlayerspecificationBuilder
    {
        private int SpecificationsId;
        private int Shooting;
        private int Defense;
        private int Skating;
        private int Physical;

        public PlayerspecificationBuilder()
        {
            SpecificationsId = 0;
            Shooting = 0;
            Defense = 0;
            Skating = 0;
            Physical = 0;
        }

        public PlayerspecificationBuilder WithSpecificationsId(int SpecificationsId)
        {
            this.SpecificationsId = SpecificationsId;
            return this;
        }
        
        public PlayerspecificationBuilder WithShooting(int Shooting)
        {
            this.Shooting = Shooting;
            return this;
        }
        
        public PlayerspecificationBuilder WithDefense(int Defense)
        {
            this.Defense = Defense;
            return this;
        }

        public PlayerspecificationBuilder WithSkating(int Skating)
        {
            this.Skating = Skating;
            return this;
        }

        public PlayerspecificationBuilder WithPhysical(int Physical)
        {
            this.Physical = Physical;
            return this;
        }

        public PlayerspecificationBL Build()
        {
            return new PlayerspecificationBL()
            {
                SpecificationsId = this.SpecificationsId,
                Shooting = this.Shooting,
                Defense = this.Defense,
                Skating = this.Skating,
                Physical = this.Physical
            };
        }
    }
}