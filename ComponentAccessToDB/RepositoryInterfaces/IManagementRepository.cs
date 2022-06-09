using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IManagementRepository : ICrudRepository<ManagementBL>
    {
        List<ManagementBL> FindByAnalytic(int id);
        List<ManagementBL> FindByManager(int id);
        ManagementBL FindManagementById(int id);
    }
}
