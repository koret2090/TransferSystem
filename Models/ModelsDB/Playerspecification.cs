using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Playerspecification
    {
        public Playerspecification()
        {
            Players = new HashSet<Player>();
        }

        public int Specificationsid { get; set; }
        public int Shooting { get; set; }
        public int Defense { get; set; }
        public int Skating { get; set; }
        public int Physical { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        
        public Playerspecification(PlayerspecificationBL playerspecification)
        {
            Specificationsid = playerspecification.SpecificationsId;
            Shooting = playerspecification.Shooting;
            Defense = playerspecification.Defense;
            Skating = playerspecification.Skating;
            Physical = playerspecification.Physical;
        }
    }
}
