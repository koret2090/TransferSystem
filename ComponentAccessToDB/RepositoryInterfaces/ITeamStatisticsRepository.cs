using System.Collections.Generic;
using System.Linq;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface ITeamStatisticsRepository : ICrudRepository<TeamstatisticBL>
    {
        TeamstatisticBL GetTeamStatisticById(int id);
        List<TeamstatisticBL> GetStatisticsByTeam(TeamBL element);
    }
}