using Models.ModelsBL;

namespace Tests.Builders
{
    public class UserBuilder
    {
        private int id;
        private string login;
        private string hash;
        private int perms;

        public UserBuilder()
        {
            id = 0;
            login = "";
            hash = "";
            perms = 0;
        }

        public UserBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }
        
        public UserBuilder WithLogin(string login)
        {
            this.login = login;
            return this;
        }
        
        public UserBuilder WithHash(string hash)
        {
            this.hash = hash;
            return this;
        }
        
        public UserBuilder WithPerms(int perms)
        {
            this.perms = perms;
            return this;
        }

        public UserInfoBL Build()
        {
            return new UserInfoBL()
            {
                Id = this.id,
                Login = this.login,
                Hash = this.hash,
                Permission = this.perms
            };
        }
    }
}