using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IDesiredPlayersRepository : ICrudRepository<DesiredplayerBL>
    {
        DesiredplayerBL GetPlayerById(int id);
        List<DesiredplayerBL> GetPlayersByManagement(ManagementBL element);
    }
}
