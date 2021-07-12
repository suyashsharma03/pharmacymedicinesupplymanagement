using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IRepo
    {
        public Credentials GetCred(Credentials cred);
    }
}
