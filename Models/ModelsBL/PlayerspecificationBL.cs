using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class PlayerspecificationBL
    {
        public int SpecificationsId { get; set; }
        public int Shooting { get; set; }
        public int Defense { get; set; }
        public int Skating { get; set; }
        public int Physical { get; set; }

        public PlayerspecificationBL(Playerspecification playerspecification)
        {
            SpecificationsId = playerspecification.Specificationsid;
            Shooting = playerspecification.Shooting;
            Defense = playerspecification.Defense;
            Skating = playerspecification.Skating;
            Physical = playerspecification.Physical;
        }
        
        public PlayerspecificationBL()
        {
            
        }
    }
}
