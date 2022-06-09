using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsDB;

namespace Models.ModelsBL
{
    public class UserInfoBL
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public int Permission { get; set; }

        public UserInfoBL(Userinfo userinfo)
        {
            Id = userinfo.Id;
            Login = userinfo.Login;
            Hash = userinfo.Hash;
            Permission = userinfo.Permission;
        }
        
        public UserInfoBL()
        {
            
        }
    }
}
