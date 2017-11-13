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
        /// return the simulation clock
        /// return null if there's no clock stored in the db
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <returns></returns>
        public Clock GetClock()
        {
            var collection = Database.GetCollection<BsonDocument>("Clock");
            var clockData = collection.Find(new BsonDocument()).FirstOrDefault();
            if (clockData != null)
            {
                Clock myClock = BsonSerializer.Deserialize<Clock>(clockData.ToJson());
                return myClock;
            }
            Console.WriteLine("clock collection is empty.");
            return null;
        }


        /// <summary>
        /// return a product based on the input product name
        /// return null if the product name does not exist
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Product GetProduct(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
            var collection = Database.GetCollection<BsonDocument>("Product");
            var product = collection.Find(filter).First();
            if (product != null)
            {
                Product myProduct = BsonSerializer.Deserialize<Product>(product.ToJson());
                return myProduct;
            }
            Console.WriteLine("Product name: " + name + " does not exist in the database.");
            return null;
        }

        /// <summary>
        /// return simulation SaveState
        /// return null if there's no SaveState stored in db
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <returns></returns>
        public SaveState GetSaveState()
        {
            var collection = Database.GetCollection<BsonDocument>("SaveState");
            var saveStateData = collection.Find(new BsonDocument()).FirstOrDefault();
            if (saveStateData != null)
            {
                SaveState mySaveState = BsonSerializer.Deserialize<SaveState>(saveStateData.ToJson());
                return mySaveState;
            }
            Console.WriteLine("SaveState collection is empty.");
            return null;
        }

        ///// <summary>
        ///// gets a person object
        ///// author: michael
        ///// date: 2017-10-15
        ///// update:
        ///// 2017-11-13 michael
        ///// changed from citizen to person. method is still the same.
        ///// </summary>
        ///// <param name="personguid"></param>
        ///// <returns>returns a list of persons as json documents</returns>
        //public person getpersonbyguid(guid personguid)
        //{
        //    var citizenscol = database.getcollection<bsondocument>("person");
        //    var filter = builders<bsondocument>.filter.eq("guid", personguid);
        //    var document = citizenscol.find(filter).first(); // find a citizen based on the filter, first result.
        //    if (document == null)
        //    {
        //        console.writeline("person guid: " + personguid + "does not exist in the collection");
        //        return null;
        //    }
        //    person person = bsonserializer.deserialize<person>(document.tojson()); // stores it in a list, deserializes the document(?)
        //    return person;
        //}

        ///// <summary>
        ///// <para>
        ///// if the getcitizens method works then this one should work.
        ///// </para>
        ///// author: michael
        ///// date: 2017-10-16
        ///// </summary>
        ///// <param name="roadid"></param>
        ///// <returns></returns>
        //public list<building> getbuildings(objectid buildid)
        //{
        //    var buildcol = database.getcollection<bsondocument>("buildings");
        //    var filter = builders<bsondocument>.filter.eq("_id", buildid);
        //    var document = buildcol.find(filter).first();
        //    list<building> building = bsonserializer.deserialize<list<building>>(document.tojson());
        //    return building;
        //}



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
            var collection = Database.GetCollection<BsonDocument>("Person");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var personListData = await collection.Find(filter).ToListAsync();
            if (personListData == null || personListData.Count == 0)
            {
                Console.WriteLine("Can not update person, guid is invalid.");
                return;
            }

            Person personToUpdate = new Person(guid, firstName, lastName, monthlyIncome, accountBalance, workplaceID, workplaceX, workplaceY, homeID, homeX, homeY, daysLeft, age, startShift, endShift);
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
            var collection = Database.GetCollection<BsonDocument>("Residential");
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

        /// <summary>
        /// Update one commercial building by guid
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
        /// <param name="retailPrice"></param>
        /// <param name="inventoryCount"></param>
        public async void UpdateCommercialBuildingByGuid(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity, int retailPrice, int inventoryCount)
        {
            var collection = Database.GetCollection<BsonDocument>("Commercial");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var commercialListData = await collection.Find(filter).ToListAsync();
            if (commercialListData == null || commercialListData.Count == 0)
            {
                Console.WriteLine("Can not update commercial building, guid is invalid.");
                return;
            }

            Commercial commercialBuildingToUpdate = new Commercial(guid, xPoint, yPoint, rating, isTall, capacity, retailPrice, inventoryCount);
            if (!DALValidator.DALCommercialBuildingValidator(commercialBuildingToUpdate))
            {
                Console.WriteLine("Can not update commercial building, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("guid", guid)
                .Set("XPoint", xPoint)
                .Set("YPoint", yPoint)
                .Set("Rating", rating)
                .Set("IsTall", isTall)
                .Set("Capacity", capacity)
                .Set("RetailPrice", retailPrice)
                .Set("InventoryCount", inventoryCount);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Update one industrial building by guid
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
        /// <param name="inventoryCount"></param>
        /// <param name="productionCost"></param>
        /// <param name="wholesalePrice"></param>
        public async void UpdateIndustrialBuildingByGuid(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity, int inventoryCount, int productionCost, int wholesalePrice)
        {
            var collection = Database.GetCollection<BsonDocument>("Industrial");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var industrialListData = await collection.Find(filter).ToListAsync();
            if (industrialListData == null || industrialListData.Count == 0)
            {
                Console.WriteLine("Can not update industrial building, guid is invalid.");
                return;
            }

            Industrial industrialBuildingToUpdate = new Industrial(guid, xPoint, yPoint, rating, isTall, capacity, inventoryCount, productionCost, wholesalePrice);
            if (!DALValidator.DALIndustrialBuildingValidator(industrialBuildingToUpdate))
            {
                Console.WriteLine("Can not update industrial building, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("guid", guid)
                .Set("XPoint", xPoint)
                .Set("YPoint", yPoint)
                .Set("Rating", rating)
                .Set("IsTall", isTall)
                .Set("Capacity", capacity)
                .Set("InventoryCount", inventoryCount)
                .Set("ProductionCost", productionCost)
                .Set("WholesalePrice", wholesalePrice);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Update one road by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="xPoint"></param>
        /// <param name="yPoint"></param>
        public async void UpdateRoadByGuid(Guid guid, int xPoint, int yPoint)
        {
            var collection = Database.GetCollection<BsonDocument>("Road");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", guid);
            var roadListData = await collection.Find(filter).ToListAsync();
            if (roadListData == null || roadListData.Count == 0)
            {
                Console.WriteLine("Can not update road, guid is invalid.");
                return;
            }

            Road roadToUpdate = new Road(guid, xPoint, yPoint);
            if (!DALValidator.DALRoadValidator(roadToUpdate))
            {
                Console.WriteLine("Can not update road, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("guid", guid)
                .Set("XPoint", xPoint)
                .Set("YPoint", yPoint);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Update clock
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="hours"></param>
        /// <param name="days"></param>
        /// <param name="years"></param>
        public async void UpdateClock(int minutes, int hours, int days, int years)
        {
            var collection = Database.GetCollection<BsonDocument>("Clock");
            var clockData = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
            if (clockData == null)
            {
                Console.WriteLine("clock collection is empty.");
                return;
            }
            var filter = Builders<BsonDocument>.Filter.Eq("_id", clockData["_id"].ToString());

            Clock clockToUpdate = new Clock(minutes, hours, days, years);
            if (!DALValidator.DALClockValidator(clockToUpdate))
            {
                Console.WriteLine("Can not update clock, at least one of the input field is invalid.");
                return;
            }
            var update = Builders<BsonDocument>.Update
                            .Set("NetMinutes", minutes)
                            .Set("NetHours", hours)
                            .Set("NetDays", days)
                            .Set("NetYears", years);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// update product by product name
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="name"></param>
        /// <param name="globalCount"></param>
        public async void UpdateProductByName(string name, int globalCount)
        {
            var collection = Database.GetCollection<BsonDocument>("Product");
            var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
            var roadListData = await collection.Find(filter).ToListAsync();
            if (roadListData == null || roadListData.Count == 0)
            {
                Console.WriteLine("Can not product, name is invalid.");
                return;
            }

            Product productToUpdate = new Product(name, globalCount);
            if (!DALValidator.DALProductValidator(productToUpdate))
            {
                Console.WriteLine("Can not update product, at least one of the input field is invalid.");
                return;
            }

            var update = Builders<BsonDocument>.Update
                .Set("Name", name)
                .Set("GlobalCount", globalCount);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// update SaveState by objectId
        /// 
        /// Author:
        /// Date:
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="saveState"></param>
        public async void UpdateSaveStateById(ObjectId _id, SaveState saveState)
        {
            var collection = Database.GetCollection<BsonDocument>("SaveState");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            var saveStateData = await collection.Find(filter).ToListAsync();

            //TODO: implementation

            return;
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
        public async void DeleteOneResidential(Industrial residential, Guid guid)
        {
            var collection = Database.GetCollection<Industrial>("Residential");
            var filter = Builders<Industrial>.Filter.Eq("guid", guid);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete one building from the collection by its id
        /// Author: Bill
        /// Updated: Steph - reflects schema changes
        /// Date: 2017-10-31
        /// </summary>
        /// <param name="_id">building id</param>
        public async void DeleteOneCommercial(Industrial commercial, Guid guid)
        {
            var collection = Database.GetCollection<Industrial>("Commercial");
            var filter = Builders<Industrial>.Filter.Eq("guid", guid);
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
