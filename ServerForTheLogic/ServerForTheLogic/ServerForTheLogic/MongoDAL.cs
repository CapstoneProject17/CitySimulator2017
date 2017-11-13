using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServerForTheLogic;
using ServerForTheLogic.DALValidator;
using ServerForTheLogic.ClientObject.Building;
using ServerForTheLogic.ClientObject;
using System;

namespace DataAccessLayer
{

    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Data Access Layer for the Server Logic and Server Communication team to talk with the database.
    /// Has standard get, update, insert, and delete operations.
    /// Author: Michael 
    /// Date: 2017-10-15
    /// Based on: http://rundevrun.blogspot.ca/2014/10/c-simple-dal-layer-with-mongodb.html
    ///           http://mongodb.github.io/mongo-csharp-driver/2.3/reference/driver/crud/
    ///           https://docs.mongodb.com/getting-started/csharp/
    ///           http://mongodb.github.io/mongo-csharp-driver/2.2/getting_started/quick_tour/
    /// </summary>
    partial class MongoDAL {
        private IMongoDatabase Database {
            get {
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("Prototype");
                return database;
            }
        }

        /* ==================== Insert Section ==================== */

        /// <summary>
        /// Insert a new Person into the Database
        /// </summary>
        /// <param name="person"></param>
        public bool InsertPerson(Person person)
        {
            if (!DALValidator.DALPersonValidator(person))
            {
                return false;
            } else
            {
                var personCol = Database.GetCollection<Person>("Person");
                personCol.InsertOne(person);
            }
            return true;
        }

        // Plural? Insert multiple citizens at once if needed...
        // Should it be a Task type with a return value?
        // this will only insert if all entries pass validation, not just the ones that do pass.
        // https://msdn.microsoft.com/en-us/library/dd235678#Remarks
        public Task InsertPeople(IEnumerable<Person> people)
        {
            foreach (Person p in people)
            {
                if (!DALValidator.DALPersonValidator(p))
                {
                    return null;
                }
            }
            var personCol = Database.GetCollection<Person>("Person");
            return personCol.InsertManyAsync(people);
        }

        /// <summary>
        /// Individual insert methods for each type of "building" to follow: 
        ///     - Residential
        ///     - Commercial
        ///     - Industrial
        ///     - Road
        /// </summary>
        /// Author: Steph
        /// <param name="building"></param>
        public bool InsertResidential(Residential residential)
        {
            if (!DALValidator.DALResidentialBuildingValidator(residential))
            {
                return false;
            } else
            {
                var residentialCol = Database.GetCollection<Residential>("Residential");
                residentialCol.InsertOne(residential);
            }
            return true;
        }

        public bool InsertCommercial(Commercial commercial)
        {
            if (!DALValidator.DALCommercialBuildingValidator(commercial))
            {
                return false;
            } else
            {
                var commercialCol = Database.GetCollection<Commercial>("Commercial");
                commercialCol.InsertOne(commercial);
            }
            return true;
        }

        public bool InsertIndustrial(Industrial industrial)
        {
            if (!DALValidator.DALIndustrialBuildingValidator(industrial))
            {
                return false;
            } else
            {
                var industrialCol = Database.GetCollection<Industrial>("Industrial");
                industrialCol.InsertOne(industrial);
            }
            return true;
        }

        // is this method necessary? 
        public bool InsertRoad(Road road)
        {
            if (!DALValidator.DALRoadValidator(road))
            {
                return false;
            } else
            {
                var roadCol = Database.GetCollection<Road>("Road");
                roadCol.InsertOne(road);
            }
            return true;
        }

        /// <summary>
        /// Inserts one product
        /// Author: Steph
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool InsertProduct(Product product)
        {
            if (!DALValidator.DALProductValidator(product))
            {
                return false;
            }
            else
            {
                var prodCol = Database.GetCollection<Product>("Product");
                prodCol.InsertOne(product);
            }
            return true;
        }

        /// <summary>
        /// TODO: Inserts a savestate at a given timestamp.
        /// Author:
        /// </summary>
        /// <param name="savestate"></param>
        /// <returns></returns>
        public bool InsertSaveState(SaveState savestate)
        {
            if (!DALValidator.DALSaveStateValidator(savestate))
            {
                return false;
            }
            else
            {
                //TODO
            }
            return true;
        }

        /// <summary>
        /// Inserts a clock (time stamp)
        /// Author: Steph
        /// May or may not be used.
        /// </summary>
        /// <param name="clock"></param>
        /// <returns></returns>
        public bool InsertClock(Clock clock)
        {
            if (!DALValidator.DALClockValidator(clock))
            {
                return false;
            }
            else
            {
                var clockCol = Database.GetCollection<Clock>("Clock");
                clockCol.InsertOne(clock);
            }
            return true;
        }

        /// <summary>
        /// purpose: insert multiple buildings
        /// Author: Bill
        /// Date:2017-10-31
        /// 
        /// Update:
        /// 2017-11-01 Bill
        ///     add validation
        ///     
        /// Update:
        /// 2017-11-13 Michael
        /// Changed method to use new validation style
        /// </summary>
        /// <param name="buildings"></param>
        public void InsertBuildings(IEnumerable<Building> buildings) {
            foreach (Building b in buildings) {
                if (!DALValidator.DALBuildingValidator(b)) {
                    System.Console.WriteLine("Building id: " + b + " did not meet validation rules.");
                    return;
                }
            }
            var buildingCol = Database.GetCollection<Building>("Buildings");
            buildingCol.InsertMany(buildings);
        }


        /* ==================== Get Section ==================== */


        /// <summary>
        /// Gets a list of Person objects
        /// Author: Michael
        /// Date: 2017-10-15
        /// Update:
        /// 2017-11-13 Michael
        /// Changed from Citizen to Person. Method is still the same.
        /// </summary>
        /// <param name="objectid"></param>
        /// <returns>Returns a list of persons as JSON documents</returns>
        public List<Person> GetCitizens(ObjectId objectid) {
            var citizensCol = Database.GetCollection<BsonDocument>("Person");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectid);
            var document = citizensCol.Find(filter).First(); // Find a citizen based on the filter, first result.
            List<Person> person = BsonSerializer.Deserialize<List<Person>>(document.ToJson()); // Stores it in a list, deserializes the document(?)
            return person;
        }

        /// <summary>
        /// <para>
        /// If the GetCitizens method works then this one should work.
        /// </para>
        /// Author: Michael
        /// Date: 2017-10-16
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public List<Building> GetBuildings(ObjectId buildId) {
            var buildCol = Database.GetCollection<BsonDocument>("Buildings");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", buildId);
            var document = buildCol.Find(filter).First();
            List<Building> building = BsonSerializer.Deserialize<List<Building>>(document.ToJson());
            return building;
        }



        /* ==================== Update Section ==================== */
        /// <summary>
        /// <para>
        /// update one citizen by its Id. 
        /// if string field is empty string then there will be no change in the original string,
        /// if int field == -1 then there will be no change in the original int value.
        /// updated value also have to meet validation rules, otherwise there's no change to the old value.
        /// </para> 
        /// 
        /// <para>
        /// reference: 
        /// how to use update: https://docs.mongodb.com/getting-started/csharp/update/
        /// Deserialize a Collection from BSON: https://www.newtonsoft.com/json/help/html/DeserializeFromBsonCollection.htm
        /// </para>
        /// 
        /// Author: Bill
        /// Date: 2017-10-31
        /// 
        /// Update: 
        /// 2017-11-13 Micahel
        /// Updated the method to use the new DAL convention. Uses PersonValidator.
        /// </summary>
        /// 
        /// <param name="_id">The ObjectId that MongoDB automatically creates</param>
        /// <param name="newFName">The new first name of the person</param>
        /// <param name="newLName"></param>
        /// <param name="newSalary"></param>
        /// <param name="newHomeID"></param>
        /// <param name="newHomeAddressX"></param>
        /// <param name="newHomeAddressY"></param>
        /// <param name="newWorplaceID"></param>
        /// <param name="newWorkAddressX"></param>
        /// <param name="newWorkAddressY"></param>
        /// <param name="newAge"></param>
        /// <param name="newDaysLeft"></param>
        public async void UpdatePersonByID(ObjectId _id, string newFName, string newLName, int newSalary, string newHomeID, int newHomeAddressX, int newHomeAddressY, string newWorplaceID, int newWorkAddressX, int newWorkAddressY, int newAge, int newDaysLeft) {
            var collection = Database.GetCollection<BsonDocument>("Person"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            var personListData = await collection.Find(filter).ToListAsync();
            if (personListData == null || personListData.Count == 0) {
                throw new System.Exception("Can not update person, _id is invalid.");
            }

            var personBSON = personListData[0];

            //get the current fields on the citizen
            string oldFName = personBSON[1].ToString();
            string oldLName = personBSON[2].ToString();
            int oldSalary = System.Convert.ToInt32(personBSON[3].ToString());
            string oldHomeID = personBSON[4].ToString();
            int oldHomeAddressX = System.Convert.ToInt32(personBSON[5].ToString());
            int oldHomeAddressY = System.Convert.ToInt32(personBSON[6].ToString());
            string oldWorkplaceID = personBSON[7].ToString();
            int oldWorkAddressX = System.Convert.ToInt32(personBSON[8].ToString());
            int oldWorkAddressY = System.Convert.ToInt32(personBSON[9].ToString());
            int oldAge = System.Convert.ToInt32(personBSON[10].ToString());
            int oldDaysLeft = System.Convert.ToInt32(personBSON[11].ToString());

            //validate changes for the fields to update
            string fName = (PersonValidator.isValidPersonFirstName(newFName)) ? newFName : oldFName;
            string lName = (PersonValidator.isValidPersonLastName(newFName)) ? newFName : oldFName;
            int salary = (PersonValidator.isValidPersonMonthlyIncome(newSalary)) ? newSalary : oldSalary;
            string homeID = (PersonValidator.isValidPersonHomeID(newHomeID)) ? newHomeID : oldHomeID;
            int homeAddressX = (PersonValidator.isValidPersonHomeX(newHomeAddressX)) ? newHomeAddressX : oldHomeAddressX;
            int homeAddressY = (PersonValidator.isValidPersonHomeY(newHomeAddressY)) ? newHomeAddressY : oldHomeAddressY;
            string workplaceID = (PersonValidator.isValidPersonWorkplaceID(newWorplaceID)) ? newWorplaceID : oldWorkplaceID;
            int workAddressX = (PersonValidator.isValidPersonWorkplaceX(newWorkAddressX)) ? newWorkAddressX : oldWorkAddressX;
            int workAddressY = (PersonValidator.isValidPersonWorkplaceX(newWorkAddressY)) ? newWorkAddressY : oldWorkAddressY;
            int age = (PersonValidator.isValidPersonAge(newAge)) ? newAge : oldAge;
            int daysLeft = (PersonValidator.isValidPersonDaysLeft(newDaysLeft)) ? newDaysLeft : oldDaysLeft;

            //update changes
            var update = Builders<BsonDocument>.Update
                .Set("First Name", fName)
                .Set("Last Name", lName)
                .Set("Salary", salary)
                .Set("HomeID", homeID)
                .Set("HomeAddressX", homeAddressX)
                .Set("HomeAddressY", homeAddressY)
                .Set("WorkplaceID", workplaceID)
                .Set("WorkAddressX", workAddressX)
                .Set("WorkAddressY", workAddressY)
                .Set("Age", age)
                .Set("DaysLeft", daysLeft);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        ///  update one building by its Id. 
        ///  if string field is empty string then there will be no change in the original string,
        ///  if int field == -1 then there will be no change in the original int value.
        ///  updated value also have to meet validation rules, otherwise there's no change to the old value.
        ///  
        ///  Author: Bill
        ///  Date: 2017-10-31
        ///  
        /// TO-DO: add validation methods
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="newBuildingName"></param>
        /// <param name="newBuildingType"></param>
        /// <param name="newBuildingCompany"></param>
        /// <param name="newBuildingLocation"></param>
        /// <param name="newBuildingMoney"></param>
        /// <param name="newBuildingLevel"></param>
        public async void UpdateBuildingById(ObjectId _id, string newBuildingName, string newBuildingType, string newBuildingCompany, int newBuildingLocation, int newBuildingMoney, int newBuildingLevel)
        {
            var collection = Database.GetCollection<BsonDocument>("Buildings"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            var buildingListData = await collection.Find(filter).ToListAsync();
            if (buildingListData == null || buildingListData.Count == 0)
            {
                throw new System.Exception("Can not update building, _id is invalid.");
            }

            var citizenBSON = buildingListData[0];

            //get the current fields on the citizen
            string oldBuildingName = citizenBSON[1].ToString();
            string oldBuildingType = citizenBSON[2].ToString();
            string oldBuildingCompany = citizenBSON[3].ToString();
            int oldBuildingLocation = System.Convert.ToInt32(citizenBSON[4].ToString());
            int oldBuildingMoney = System.Convert.ToInt32(citizenBSON[5].ToString());
            int oldBuildingLevel = System.Convert.ToInt32(citizenBSON[6].ToString());

            //validate changes for the fields to update
            string buildingName = (DALValidator.isValidBuildingName(newBuildingName)) ? newBuildingName : oldBuildingName;
            string buildingType = (DALValidator.isValidBuildingType(newBuildingType)) ? newBuildingType : oldBuildingType;
            string buildingCompany = (DALValidator.isValidBuildingCompany(newBuildingCompany)) ? newBuildingCompany : oldBuildingCompany;
            int buildingLocation = (DALValidator.isValidBuildingLocation(newBuildingLocation)) ? newBuildingLocation : oldBuildingLocation;
            int buildingMoney = (DALValidator.isValidBuildingMoney(newBuildingMoney)) ? newBuildingMoney : oldBuildingMoney;
            int buildingLevel = (DALValidator.isValidBuildingLevel(newBuildingLevel)) ? newBuildingLevel : oldBuildingLevel;

            //update changes
            var update = Builders<BsonDocument>.Update
                .Set("BuildingName", buildingName)
                .Set("BuildingType", buildingType)
                .Set("BuildingCompany", buildingCompany)
                .Set("BuildingLocation", buildingLocation)
                .Set("buildingMoney", buildingMoney)
                .Set("BuildingLevel", buildingLevel);
            var result = await collection.UpdateOneAsync(filter, update);
        }


        /// <summary>
        /// Updates the citizens age.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="citizen"></param>
        public async void UpdateOneCitizen(ObjectId _id, int newAge) {
            var collection = Database.GetCollection<BsonDocument>("Citizens"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            var update = Builders<BsonDocument>.Update.Set("Age", newAge);
            var result = await collection.UpdateOneAsync(filter, update);
        }


        /* ==================== Delete Section ==================== */

        /// <summary>
        /// Delete one person from the collection.
        /// Author: Michael
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-15
        /// </summary>
        /// <param name="_id"></param>
        public async void DeleteOnePerson(Guid guid) {
            var collection = Database.GetCollection<Person>("Person");
            //what to do here?
            var filter = Builders<Person>.Filter.Eq("guid", guid);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete one building from the collection by its id
        /// Author: Bill
        /// Updated: Steph - reflects schema changes 
        /// Date: 2017-10-31
        /// </summary>
        /// <param name="_id">building id</param>
        public async void DeleteOneResidential(Residential residential, Guid guid)
        {
            var collection = Database.GetCollection<Residential>("Residential");
            var filter = Builders<Residential>.Filter.Eq("guid", guid);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete one building from the collection by its id
        /// Author: Bill
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-31
        /// </summary>
        /// <param name="_id">building id</param>
        public async void DeleteOneCommercial(Commercial commercial, Guid guid)
        {
            var collection = Database.GetCollection<Commercial>("Commercial");
            var filter = Builders<Commercial>.Filter.Eq("guid", guid);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete one building from the collection by its id
        /// Author: Bill
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-31
        /// </summary>
        /// <param name="_id">building id</param>
        public async void DeleteOneIndustrial(Industrial industrial, Guid guid)
        {
            var collection = Database.GetCollection<Industrial>("Industrial");
            var filter = Builders<Industrial>.Filter.Eq("guid", guid);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete all citizens (drop then recreate table)
        /// Author: Bill
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-31
        /// </summary>
        public void DeleteAllCitizens()
        {
            Database.DropCollection("Person");
            Database.CreateCollection("Person");
        }

        /// <summary>
        /// Delete all residential type buildings (drop then recreate table)
        /// Author: Steph
        /// Date: 
        /// </summary>
        public void DeleteAllResidential()
        {
            Database.DropCollection("Residential");
            Database.CreateCollection("Residential");
        }

        /// <summary>
        /// Delete all commercial type buildings (drop then recreate table)
        /// Author: Steph
        /// Date: 
        /// </summary>
        public void DeleteAllCommercial()
        {
            Database.DropCollection("Commercial");
            Database.CreateCollection("Commercial");
        }

        /// <summary>
        /// Delete all industrial type buildings (drop then recreate table)
        /// Author: Steph
        /// Date: 
        /// </summary>
        public void DeleteAllIndustrial()
        {
            Database.DropCollection("Industrial");
            Database.CreateCollection("Industrial");
        }

        /// <summary>
        /// Delete all roads (drop then recreate table)
        /// Author: Steph
        /// Date: 
        /// </summary>
        public void DeleteAllRoads()
        {
            Database.DropCollection("Road");
            Database.CreateCollection("Road");
        }

        /// <summary>
        /// Delete all products (drop then recreate table)
        /// Author: Steph
        /// Date: 
        /// </summary>
        public void DeleteAllProducts()
        {
            Database.DropCollection("Product");
            Database.CreateCollection("Product");
        }
    }
}
