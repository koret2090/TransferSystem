using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Player
    {
        public Player()
        {
            Availabledeals = new HashSet<Availabledeal>();
            Desiredplayers = new HashSet<Desiredplayer>();
        }

        public int Playerid { get; set; }
        public int? Teamid { get; set; }
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

        public virtual Playerspecification PlayerspecificationsNavigation { get; set; }
        public virtual Playerstatistic PlayerstatisticsNavigation { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Availabledeal> Availabledeals { get; set; }
        public virtual ICollection<Desiredplayer> Desiredplayers { get; set; }
        
        public Player(PlayerBL player)
        {           
            Playerid = player.PlayerId;
            Teamid = player.TeamId;
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
