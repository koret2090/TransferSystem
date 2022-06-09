using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class PlayerDTO
    {
        public int PlayerId { get; set; }
        public int? TeamId { get; set; }
        public int? Playerstatistics { get; set; }
        public int? Playerspecifications { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Number { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int Cost { get; set; }

        public PlayerDTO()
        {
            
        }

        public PlayerDTO(PlayerBL player)
        {           
            PlayerId = player.PlayerId;
            TeamId = player.TeamId;
            Playerstatistics = player.Playerstatistics;
            Playerspecifications = player.Playerspecifications;
            Name = player.Name;
            Position = player.Position;
            Weight = player.Weight;
            Height = player.Height;
            Number = player.Number;
            Age = player.Age;
            Country = player.Country;
            Cost = player.Cost;
        }
    }
}
