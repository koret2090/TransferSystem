using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;
using Models.ModelsDB;

namespace Models.ModelsDTO
{
    public partial class DesiredplayerDTO
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? Managementid { get; set; }
        public string PlayerName { get; set; }
        public string TeamName { get; set; }

        public DesiredplayerDTO()
        {
            
        }
        
        public DesiredplayerDTO(DesiredplayerBL desiredplayer)
        {
            Id = desiredplayer.Id;
            PlayerId = desiredplayer.PlayerId;
            Managementid = desiredplayer.Managementid;
            PlayerName = null;
            TeamName = null;
        }
    }
}
