using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IPlayerRepository : ICrudRepository<PlayerBL>
    {
        List<PlayerBL> GetPlayersByTeam(TeamBL element);
        PlayerBL FindPlayerById(int id);
        List<PlayerBL> FindPlayerByName(string name); 
    }
}
