using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class DesiredplayerBL
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? Managementid { get; set; }
        
        public DesiredplayerBL(Desiredplayer desiredplayer)
        {
            Id = desiredplayer.Id;
            PlayerId = desiredplayer.Playerid;
            Managementid = desiredplayer.Managementid;
        }
        
        public DesiredplayerBL()
        {
            
        }
    }
}
