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
    partial class MongoDAL
    {
        private IMongoDatabase Database
        {
            get
            {
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
            }
            else
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
            }
            else
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
            }
            else
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
            }
            else
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
            }
            else
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
        public void InsertBuildings(IEnumerable<Building> buildings)
        {
            foreach (Building b in buildings)
            {
                if (!DALValidator.DALBuildingValidator(b))
                {
                    System.Console.WriteLine("Building id: " + b.Guid.ToString() + " did not meet validation rules.");
                    return;
                }
            }

            //TODO: we need to decide if inserting collection of buildings is needed

            //var buildingCol = Database.GetCollection<Building>("Buildings");
            //buildingCol.InsertMany(buildings);
        }


        /* ==================== Get Section ==================== */
        //TODO: 2017-11-13 do we need getter for all objects in db? currently we only have getters for objects with guid

        /// <summary>
        /// Return an object based on the input guid
        /// return null if there's no object with the input guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Object GetObjectByGuid(Guid guid)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            foreach (var collectionName in Database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                var collection = Database.GetCollection<BsonDocument>(collectionName.ToString());
                var document = collection.Find(filter).First();
                if (document != null)
                {
                    Object myObj = BsonSerializer.Deserialize<Object>(document.ToJson());
                    return myObj;
                }
            }
            Console.WriteLine("Guid: " + guid + "does not exist in the database.");
            return null;
        }


        /// <summary>
        /// Gets a person object
        /// Author: Michael
        /// Date: 2017-10-15
        /// Update:
        /// 2017-11-13 Michael
        /// Changed from Citizen to Person. Method is still the same.
        /// </summary>
        /// <param name="personGuid"></param>
        /// <returns>Returns a list of persons as JSON documents</returns>
        public Person GetPersonByGuid(Guid personGuid)
        {
            var citizensCol = Database.GetCollection<BsonDocument>("Person");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", personGuid);
            var document = citizensCol.Find(filter).First(); // Find a citizen based on the filter, first result.
            if (document == null)
            {
                Console.WriteLine("Person Guid: " + personGuid + "does not exist in the collection");
                return null;
            }
            Person person = BsonSerializer.Deserialize<Person>(document.ToJson()); // Stores it in a list, deserializes the document(?)
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
        public List<Building> GetBuildings(ObjectId buildId)
        {
            var buildCol = Database.GetCollection<BsonDocument>("Buildings");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", buildId);
            var document = buildCol.Find(filter).First();
            List<Building> building = BsonSerializer.Deserialize<List<Building>>(document.ToJson());
            return building;
        }



        /* ==================== Update Section ==================== */
        /*
         * Update rule: object can not be updated if one of the input fields doesnot pass validation.
         * 
         * reference: 
         * how to use update: https://docs.mongodb.com/getting-started/csharp/update/
         * Deserialize a Collection from BSON: https://www.newtonsoft.com/json/help/html/DeserializeFromBsonCollection.htm
         * 
         */

        /// <summary>
        /// Update one person by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="monthlyIncome"></param>
        /// <param name="accountBalance"></param>
        /// <param name="workplaceID"></param>
        /// <param name="workplaceX"></param>
        /// <param name="workplaceY"></param>
        /// <param name="homeID"></param>
        /// <param name="homeX"></param>
        /// <param name="homeY"></param>
        /// <param name="daysLeft"></param>
        /// <param name="age"></param>
        /// <param name="startShift"></param>
        /// <param name="endShift"></param>
        public async void UpdatePersonByGuid(Guid guid, string firstName, string lastName, int monthlyIncome, int accountBalance, string workplaceID, int workplaceX, int workplaceY, string homeID, int homeX, int homeY, int daysLeft, int age, int startShift, int endShift)
        {
            var collection = Database.GetCollection<BsonDocument>("Person"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var personListData = await collection.Find(filter).ToListAsync();
            if (personListData == null || personListData.Count == 0)
            {
                Console.WriteLine("Can not update person, guid is invalid.");
                return;
            }

            Person personToUpdate = new Person(firstName, lastName, monthlyIncome, accountBalance, workplaceID, workplaceX, workplaceY, homeID, homeX, homeY, daysLeft, age, startShift, endShift);
            if (!DALValidator.DALPersonValidator(personToUpdate))
            {
                Console.WriteLine("Can not update person, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("guid", guid)
                .Set("FirstName", firstName)
                .Set("LastName", lastName)
                .Set("MonthlyIncome", monthlyIncome)
                .Set("AccountBalance", accountBalance)
                .Set("WorkplaceID", workplaceID)
                .Set("WorkplaceX", workplaceX)
                .Set("WorkplaceY", workplaceY)
                .Set("HomeID", homeID)
                .Set("HomeX", homeX)
                .Set("HomeY", homeY)
                .Set("DaysLeft", daysLeft)
                .Set("Age", age)
                .Set("StartShift", startShift)
                .Set("EndShift", endShift);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Update one residential building by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="xPoint"></param>
        /// <param name="yPoint"></param>
        /// <param name="rating"></param>
        /// <param name="isTall"></param>
        /// <param name="capacity"></param>
        public async void UpdateResidentialBuildingByGuid(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity)
        {
            var collection = Database.GetCollection<BsonDocument>("Residential"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var residentialListData = await collection.Find(filter).ToListAsync();
            if (residentialListData == null || residentialListData.Count == 0)
            {
                Console.WriteLine("Can not update residential building, guid is invalid.");
                return;
            }

            Residential residentialBuildingToUpdate = new Residential(guid, xPoint, yPoint, rating, isTall, capacity);
            if (!DALValidator.DALResidentialBuildingValidator(residentialBuildingToUpdate))
            {
                Console.WriteLine("Can not update residential building, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("guid", guid)
                .Set("XPoint", xPoint)
                .Set("YPoint", yPoint)
                .Set("Rating", rating)
                .Set("IsTall", isTall)
                .Set("Capacity", capacity);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        public async void UpdateCommercialBuildingByGuid()
        {

        }

        /* ==================== Delete Section ==================== */

        /// <summary>
        /// Delete one person from the collection.
        /// Author: Michael
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-15
        /// </summary>
        /// <param name="_id"></param>
        public async void DeleteOnePerson(Guid guid)
        {
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
        /// Date: 2017-11-13
        /// </summary>
        public void DeleteAllResidential()
        {
            Database.DropCollection("Residential");
            Database.CreateCollection("Residential");
        }

        /// <summary>
        /// Delete all commercial type buildings (drop then recreate table)
        /// Author: Steph
        /// Date: 2017-11-13
        /// </summary>
        public void DeleteAllCommercial()
        {
            Database.DropCollection("Commercial");
            Database.CreateCollection("Commercial");
        }

        /// <summary>
        /// Delete all industrial type buildings (drop then recreate table)
        /// Author: Steph
        /// Date: 2017-11-13
        /// </summary>
        public void DeleteAllIndustrial()
        {
            Database.DropCollection("Industrial");
            Database.CreateCollection("Industrial");
        }

        /// <summary>
        /// Delete all roads (drop then recreate table)
        /// Author: Steph
        /// Date: 2017-11-13
        /// </summary>
        public void DeleteAllRoads()
        {
            Database.DropCollection("Road");
            Database.CreateCollection("Road");
        }

        /// <summary>
        /// Delete all products (drop then recreate table)
        /// Author: Steph
        /// Date: 2017-11-13
        /// </summary>
        public void DeleteAllProducts()
        {
            Database.DropCollection("Product");
            Database.CreateCollection("Product");
        }
    }
}
