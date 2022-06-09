using Models.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.ModelsBL;

namespace Models.ModelsDTO
{
    public partial class UserInfoDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public int Permission { get; set; }

        public UserInfoDTO()
        {
            
        }

        public UserInfoDTO(UserInfoBL userinfo)
        {
            Id = userinfo.Id;
            Login = userinfo.Login;
            Hash = userinfo.Hash;
            Permission = userinfo.Permission;
        }
    }
}
