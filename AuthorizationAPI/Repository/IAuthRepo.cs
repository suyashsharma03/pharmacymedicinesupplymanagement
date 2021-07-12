using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IAuthRepo
    {
        public string GenerateJSONWebToken(Credentials userInfo);
        public Credentials AuthenticateUser(Credentials login);
    }
}
