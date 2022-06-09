using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IPlayerStatisticsRepository : ICrudRepository<PlayerstatisticBL>
    {
        PlayerstatisticBL GetPlayerStatisticById(int id);
        List<PlayerstatisticBL> GetStatisticsByPlayer(PlayerBL element);
    }
}
