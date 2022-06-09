using System;
using System.Collections.Generic;
using Models.ModelsBL;

#nullable disable

namespace Models.ModelsDB
{
    public partial class Desiredplayer
    {
        public int Id { get; set; }
        public int? Playerid { get; set; }
        public int? Managementid { get; set; }

        public virtual Management Management { get; set; }
        public virtual Player Player { get; set; }
        
        public Desiredplayer(DesiredplayerBL desiredplayer)
        {
            Id = desiredplayer.Id;
            Playerid = desiredplayer.PlayerId;
            Managementid = desiredplayer.Managementid;
        }
        
        public Desiredplayer()
        {
            
        }
    }
}
