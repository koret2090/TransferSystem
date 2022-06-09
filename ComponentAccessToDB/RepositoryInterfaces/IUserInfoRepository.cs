using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface IUserInfoRepository : ICrudRepository<UserInfoBL>
    {
        List<UserInfoBL> FindUserByLogin(string login);
        UserInfoBL FindUserById(int id);
    }
}
