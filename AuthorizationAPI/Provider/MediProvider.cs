using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class MediProvider : IProvider
    {
        private static List<Credentials> List = new List<Credentials>()
        {
            new Credentials{ Username = "user1", Password = "user1"},
            new Credentials{ Username = "user2", Password = "user2"}
        };
        public List<Credentials> GetList()
        {
            return List;
        }

        public Credentials GetUser(Credentials cred)
        {
            List<Credentials> rList = GetList();
            Credentials Ucred = rList.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

            return Ucred;
        }
    }
}
