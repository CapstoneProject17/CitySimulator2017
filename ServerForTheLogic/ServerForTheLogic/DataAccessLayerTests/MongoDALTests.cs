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
    /// <summary>
    /// MongoDALTests
    /// Team: DB
    /// Unit tests for CRUD functions to DB.
    /// Author: Michael, Sean, Stephanie, Bill 
    /// Date: 2017-11-21
    /// Based on: https://msdn.microsoft.com/en-us/library/hh694602.aspx
    /// </summary>
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
            Person returnedPerson = (Person)db.GetObjectByGuid(newGuid);
            Assert.AreEqual(returnedPerson.Guid, newGuid);
        }

        [TestMethod()]
        public void InsertProductTest()
        {
            //Arrange
            MongoDAL db = new MongoDAL();
            Product testProduct = new Product("test_product_1", 50);

            //Act
            db.InsertProduct(testProduct);

            //Assert
            Product returnedProduct = db.GetProduct("test_product_1");
            Console.WriteLine(returnedProduct.Name);
            Assert.AreEqual(returnedProduct.GlobalCount, 50);
        }

        [TestMethod()]
        public void InsertClockTest() {
            // Arrange
            MongoDAL db = new MongoDAL();
            Clock clockTest = new Clock(22, 14, 30, 1);

            // Act
            db.InsertClock(clockTest);

            // Assert
            Clock returnedClock = db.GetClock();
            Console.WriteLine(returnedClock.NetMinutes);
            Assert.AreEqual(returnedClock.NetMinutes, 22);
            Assert.AreEqual(returnedClock.NetHours, 14);
            Assert.AreEqual(returnedClock.NetDays, 30);
        }

        [TestMethod()]
        public void UpdateClockTest() {
            // Arrange
            MongoDAL db = new MongoDAL();
            //Clock clockTest = new Clock(12, 20, 15, 2);

            // Act
            db.UpdateClock(12, 20, 15, 2);

            Clock updatedClock = db.GetClock();
            Assert.AreEqual(updatedClock.NetMinutes, 12);
            Assert.AreEqual(updatedClock.NetHours, 20);
            Assert.AreEqual(updatedClock.NetDays, 15);
            Assert.AreEqual(updatedClock.NetYears, 2);
        }

        [TestMethod()]
        public void UpdateProductTest() {
            // Arrange
            MongoDAL db = new MongoDAL();

            // Act
            db.UpdateProductByName("test_product_1", 60);

            Product productTest = db.GetProduct("test_product_1");
            Assert.AreEqual(productTest.GlobalCount, 60);
        }
    }
}