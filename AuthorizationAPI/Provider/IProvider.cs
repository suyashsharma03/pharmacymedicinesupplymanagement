using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public interface IProvider
    {
        public List<Credentials> GetList();

        public Credentials GetUser(Credentials cred);
    }
}
