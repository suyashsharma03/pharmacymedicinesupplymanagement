using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AuthorizationTest
{
    [TestFixture]
    public class RepoTest
    {
        List<Credentials> cred;
        [SetUp]
        public void BaseWork()
        {
            cred = new List<Credentials>()
            {
                new Credentials(){ Username = "user1", Password = "user1"},
                null
            };
        }

        [TearDown]
        public void EndWork()
        {
            cred = null;
        }

        [TestCase("user1","user1")]
        public void GetCred_Returns_Object(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IRepo> moq = new Mock<IRepo>();
            moq.Setup(p => p.GetCred(auth)).Returns(cred[0]);
            var testCred = moq.Object.GetCred(auth);
            Assert.IsNotNull(testCred);
        }

        [TestCase("user3", "user3")]
        public void GetCred_Returns_Null(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IRepo> moq = new Mock<IRepo>();
            moq.Setup(p => p.GetCred(auth)).Returns(cred[1]);
            var testCred = moq.Object.GetCred(auth);
            Assert.IsNull(testCred);
        }
    }
}
