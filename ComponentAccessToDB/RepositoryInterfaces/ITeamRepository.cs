using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface ITeamRepository : ICrudRepository<TeamBL>
    {
        TeamBL FindTeamById(int id);
        List<TeamBL> FindTeamByName(string name);
        List<TeamBL> FindTeamByPlayer(PlayerBL player);
        List<TeamBL> FindTeamByManagement(int managementID);
    }
}
