using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class PlayerspecificationDTO
    {
        public int SpecificationsId { get; set; }
        public int Shooting { get; set; }
        public int Defense { get; set; }
        public int Skating { get; set; }
        public int Physical { get; set; }

        public PlayerspecificationDTO()
        {
            
        }

        public PlayerspecificationDTO(PlayerspecificationBL playerspecification)
        {
            SpecificationsId = playerspecification.SpecificationsId;
            Shooting = playerspecification.Shooting;
            Defense = playerspecification.Defense;
            Skating = playerspecification.Skating;
            Physical = playerspecification.Physical;
        }
    }
}
