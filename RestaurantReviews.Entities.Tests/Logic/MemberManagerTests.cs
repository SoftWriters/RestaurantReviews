using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Entities.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic.Tests
{
    [TestClass()]
    public class MemberManagerTests
    {
        [TestMethod()]
        public void CreateMemberTest()
        {
            Member createdmember = MemberManager.CreateMember("username", "firstname", "lastname", "email");

            Member retrievedmember = MemberManager.GetMember(createdmember.Id);

            Assert.AreEqual(retrievedmember.Id, createdmember.Id);
            Assert.AreEqual(retrievedmember.UserName, createdmember.UserName);
            Assert.AreEqual(retrievedmember.FirstName, createdmember.FirstName);
            Assert.AreEqual(retrievedmember.LastName, createdmember.LastName);
            Assert.AreEqual(retrievedmember.Email, createdmember.Email);
        }

        [TestMethod()]
        public void UpdateMemberTest()
        {
            Member createdmember = MemberManager.CreateMember("username", "firstname", "lastname", "email");

            MemberManager.UpdateMember(createdmember.Id, "1", "2", "3", "4");

            Member retrievedmember = MemberManager.GetMember(createdmember.Id);

            Assert.AreEqual(retrievedmember.Id, createdmember.Id);
            Assert.AreEqual(retrievedmember.UserName, "1");
            Assert.AreEqual(retrievedmember.FirstName, "2");
            Assert.AreEqual(retrievedmember.LastName, "3");
            Assert.AreEqual(retrievedmember.Email, "4");
        }

        [TestMethod()]
        [ExpectedException(typeof(RestaurantReviews.Entities.Data.RetrievalException))]
        public void DeleteMemberTest()
        {
            //RetrievalException
            Member createdmember = MemberManager.CreateMember("username", "firstname", "lastname", "email");

            MemberManager.DeleteMember(createdmember);

            MemberManager.GetMember(createdmember.Id);

            Assert.Fail();
        }

        [TestMethod()]
        public void GetMemberTest()
        {
            Member createdmember = MemberManager.CreateMember("username", "firstname", "lastname", "email");

            Member retrievedmember = MemberManager.GetMember(createdmember.Id);

            Assert.AreEqual(retrievedmember.Id, createdmember.Id);
            Assert.AreEqual(retrievedmember.UserName, createdmember.UserName);
            Assert.AreEqual(retrievedmember.FirstName, createdmember.FirstName);
            Assert.AreEqual(retrievedmember.LastName, createdmember.LastName);
            Assert.AreEqual(retrievedmember.Email, createdmember.Email);
        }
    }
}