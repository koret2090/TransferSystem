using System.Collections.Generic;
using System.Linq;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IPlayerSpecificationsRepository : ICrudRepository<PlayerspecificationBL>
    {
        PlayerspecificationBL GetPlayerSpecificationById(int id);
        List<PlayerspecificationBL> GetSpecificationByPlayer(PlayerBL element);
    }
}