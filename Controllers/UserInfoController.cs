using System.Collections.Generic;
using System.Linq;
using Models.ModelsDB;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;

namespace Controllers
{
    public class UserInfoController
    {
        private readonly IUserInfoRepository _userInfoRepository;
        
        public UserInfoController(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public bool Add(UserInfoBL user)
        {
            return _userInfoRepository.Add(user);
        }

        public List<UserInfoBL> GetAll()
        {
            var users = _userInfoRepository.GetAll();
            return users.Any() ? users.ToList() : null;
        }

        public bool Update(UserInfoBL user)
        {
            return _userInfoRepository.Update(user);
        }

        public StatusCode Delete(int id)
        {
            return _userInfoRepository.Delete(id);
        }

        public UserInfoBL FindUserByLogin(string login)
        {
            var user = _userInfoRepository.FindUserByLogin(login);
            return user.Any() ? user.First() : null;
        }

        public UserInfoBL FindUserById(int id)
        {
            return _userInfoRepository.FindUserById(id);
        }
    }
}