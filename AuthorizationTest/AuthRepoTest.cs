using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationTest
{
    [TestFixture]
    class AuthRepoTest
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

        [TestCase("user1", "user1")]
        public void AuthenticateUser_Returns_Object(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IAuthRepo> moq = new Mock<IAuthRepo>();
            moq.Setup(p => p.AuthenticateUser(auth)).Returns(cred[0]);
            var testCred = moq.Object.AuthenticateUser(auth);
            Assert.IsNotNull(testCred);
        }

        [TestCase("user3", "user3")]
        public void AuthenticateUser_Returns_Null(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IAuthRepo> moq = new Mock<IAuthRepo>();
            moq.Setup(p => p.AuthenticateUser(auth)).Returns(cred[1]);
            var testCred = moq.Object.AuthenticateUser(auth);
            Assert.IsNull(testCred);
        }
    }
}
