using AuthorizationAPI.Model;
using AuthorizationAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class Repo : IRepo
    {
        private IProvider _provider;
        public Repo(IProvider provider)
        {
            _provider = provider;
        }
        public Credentials GetCred(Credentials cred)
        {
            if (cred == null)
            {
                return null;
            }
            Credentials Ucred = _provider.GetUser(cred);
            return Ucred;
        }
    }
}
