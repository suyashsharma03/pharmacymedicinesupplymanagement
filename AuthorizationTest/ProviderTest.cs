using AuthorizationAPI.Model;
using AuthorizationAPI.Provider;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationTest
{
    [TestFixture]
    class ProviderTest
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
        public void GetUser_Returns_Object(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IProvider> moq = new Mock<IProvider>();
            moq.Setup(p => p.GetUser(auth)).Returns(cred[0]);
            var testCred = moq.Object.GetUser(auth);
            Assert.IsNotNull(testCred);
        }

        [TestCase("user3", "user3")]
        public void GetUser_Returns_Null(string user, string pass)
        {
            Credentials auth = new Credentials() { Username = user, Password = pass };
            Mock<IProvider> moq = new Mock<IProvider>();
            moq.Setup(p => p.GetUser(auth)).Returns(cred[1]);
            var testCred = moq.Object.GetUser(auth);
            Assert.IsNull(testCred);
        }

        [TestCase]
        public void GetList_Test()
        {
            Mock<IProvider> moq = new Mock<IProvider>();
            moq.Setup(p => p.GetList()).Returns(cred);
            var testCred = moq.Object.GetList();
            Assert.IsNotNull(testCred);
        }
    }
}
