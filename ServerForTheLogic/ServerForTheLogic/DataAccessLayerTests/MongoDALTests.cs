using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerForTheLogic.ClientObject;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class MongoDALTests
    {
        [TestMethod()]
        public void InsertPersonTest()
        {
            //Arrange
            MongoDAL db = new MongoDAL();
            Guid newGuid = Guid.NewGuid();
            Person testPerson = new Person(newGuid, "Fn", "Ln", 100, 200, "test_workplace_id", 1, 1, "test_home_id", 2, 2, 100, 50, 7, 9);

            //Act
            db.InsertPerson(testPerson);

            //Assert
            Person returnedPerson = (Person) db.GetObjectByGuid(newGuid);
            Assert.Equals(returnedPerson.Guid, newGuid);
        }
    }
}