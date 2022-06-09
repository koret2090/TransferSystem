using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB.RepositoryInterfaces;
using Models;
using Models.ModelsBL;
using Models.ModelsDB;

namespace ComponentAccessToDB.RepositoryImplementation
{
    public class UserInfoRepository : IUserInfoRepository, IDisposable
    {
        private readonly transfersystemContext db;
        
        public UserInfoRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        
        public bool Add(UserInfoBL element)
        {
            try
            {
                var elem = new Userinfo(element);
                elem.Id = db.Userinfos.Count() + 1;
                db.Userinfos.Add(elem);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public List<UserInfoBL> GetAll()
        {
            return ConvertToBL(db.Userinfos);
        }
        
        public bool Update(UserInfoBL element)
        {
            try
            {
                var elem = new Userinfo(element);
                db.Userinfos.Update(elem);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public StatusCode Delete(int id)
        {
            try
            {
                var elem = db.Userinfos.Find(id);
                if (elem == null)
                {
                    return StatusCode.NotFound;
                }
                db.Userinfos.Remove(elem);
                db.SaveChanges();
                return StatusCode.Ok;
            }
            catch
            {
                return StatusCode.BadRequest;
            }
        }
        
        public List<UserInfoBL> FindUserByLogin(string login)
        {
            return ConvertToBL(db.Userinfos.Where(needed => needed.Login == login));
        }
        
        public UserInfoBL FindUserById(int id)
        {
            var res = db.Userinfos.Find(id);
            if (res != null) 
                return new UserInfoBL(res);
            return null;
        }
        
        private List<UserInfoBL> ConvertToBL(IQueryable<Userinfo> args)
        {
            List<UserInfoBL> elements = new List<UserInfoBL>();

            
            foreach (var elem in args)
            {
                elements.Add(new UserInfoBL(elem));
            }

            return elements;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
