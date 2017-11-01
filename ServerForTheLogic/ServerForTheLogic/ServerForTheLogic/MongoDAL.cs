using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


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
        /// Insert a new Citizen into the Database
        /// </summary>
        /// <param name="citizen"></param>
        public void InsertCitizen(Citizens citizen) {
            if (DALValidator.CitizenValidator(citizen))
            {
                var citizensCol = Database.GetCollection<Citizens>("Citizens");
                citizensCol.InsertOne(citizen);
            }
        }

        // Plural? Insert multiple citizens at once if needed...
        // Should it be a Task type with a return value?
        // https://msdn.microsoft.com/en-us/library/dd235678#Remarks
        public Task InsertCitizens(IEnumerable<Citizens> citizens) {
            foreach(Citizens c in citizens)
            {
                if (!DALValidator.CitizenValidator(c))
                {
                    return null;
                }
            }
            var citizensCol = Database.GetCollection<Citizens>("Citizens");
            return citizensCol.InsertManyAsync(citizens);
        }

        /// <summary>
        /// purpose: insert one building
        /// Author: Bill
        /// Date: 2017-10-31
        /// 
        /// Update:
        /// 2017-11-01 Bill
        ///     add validation
        /// </summary>
        /// <param name="building"></param>
        public void InsertBuilding(Buildings building) {
            if (DALValidator.BuildingValidator(building))
            {
                var buildingCol = Database.GetCollection<Buildings>("Buildings");
                buildingCol.InsertOne(building);
            }
        }

        /// <summary>
        /// purpose: insert multiple buildings
        /// Author: Bill
        /// Date:2017-10-31
        /// 
        /// Update:
        /// 2017-11-01 Bill
        ///     add validation
        /// </summary>
        /// <param name="buildings"></param>
        public void InsertBuildings(IEnumerable<Buildings> buildings)
        {
            foreach (Buildings b in buildings)
            {
                if (!DALValidator.BuildingValidator(b))
                {
                    System.Console.WriteLine("Building id: " + b.Id + " did not meet validation rules.");
                    return;
                }
            }
            var buildingCol = Database.GetCollection<Buildings>("Buildings");
            buildingCol.InsertMany(buildings);
        }
        

        public void InsertSaveState(SaveState save) {
            var saveStateCol = Database.GetCollection<SaveState>("SaveState");
            saveStateCol.InsertOne(save);
        }

        /* ==================== Get Section ==================== */

        // Would this work?
        // No, I don't think it will... Would it just get one citizen? I think so...
        public List<Citizens> GetCitizens(ObjectId objectid) {  
            var citizensCol = Database.GetCollection<BsonDocument>("Citizens");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectid);
            var document = citizensCol.Find(filter).First(); // Find a citizen based on the filter, first result.
            List<Citizens> citizen = BsonSerializer.Deserialize<List<Citizens>>(document.ToJson()); // Stores it in a list, deserializes the document(?)
            return citizen;
        }

        /// <summary>
        /// If the GetCitizens method works then this one should work. YOLO
        /// Author: Michael
        /// Date: 2017-10-16
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public List<Buildings> GetBuildings(ObjectId buildId) {
            var buildCol = Database.GetCollection<BsonDocument>("Buildings");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", buildId);
            var document = buildCol.Find(filter).First();
            List<Buildings> building = BsonSerializer.Deserialize<List<Buildings>>(document.ToJson());
            return building;
        }



        /* ==================== Update Section ==================== */
        // reference: 
        // how to use update: https://docs.mongodb.com/getting-started/csharp/update/
        // Deserialize a Collection from BSON: https://www.newtonsoft.com/json/help/html/DeserializeFromBsonCollection.htm

        /// <summary>
        ///  update one citizen by its Id. 
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
        /// <param name="newName"></param>
        /// <param name="newSalary"></param>
        /// <param name="newHomeAddress"></param>
        /// <param name="newWorkAddress"></param>
        /// <param name="newAge"></param>
        /// <param name="newDaysLeftToLive"></param>
        public async void UpdateCitizenByID(ObjectId _id, string newName, int newSalary, int newHomeAddress, int newWorkAddress, int newAge, int newDaysLeftToLive)
        {
            var collection = Database.GetCollection<BsonDocument>("Citizens"); // Should it be a BSON document or a collection of Citizens?
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            var citizenListData = await collection.Find(filter).ToListAsync();
            if (citizenListData == null || citizenListData.Count == 0)
            {
                throw new System.Exception("Can not update citizen, _id is invalid.");
            }
            
            var citizenBSON = citizenListData[0];

            //get the current fields on the citizen
            string oldName = citizenBSON[1].ToString();
            int oldSalary = System.Convert.ToInt32(citizenBSON[2].ToString());
            int oldHomeAddress = System.Convert.ToInt32(citizenBSON[3].ToString());
            int oldWorkAddress = System.Convert.ToInt32(citizenBSON[4].ToString());
            int oldAge = System.Convert.ToInt32(citizenBSON[5].ToString());
            int oldDaysLeftToLive = System.Convert.ToInt32(citizenBSON[6].ToString());

            //validate changes for the fields to update
            string name = (DALValidator.isValidCitizenName(newName)) ? newName : oldName;
            int salary = (DALValidator.isValidCitizenSalary(newSalary)) ? newSalary : oldSalary;
            int homeAddress = (DALValidator.isValidCitizenHomeAddress(newHomeAddress)) ? newHomeAddress : oldHomeAddress;
            int workAddress = (DALValidator.isValidCitizenWorkAddress(newWorkAddress)) ? newWorkAddress : oldWorkAddress;
            int age = (DALValidator.isValidCitizenAge(newAge)) ? newAge : oldAge;
            int daysLeftToLive = (DALValidator.isValidCitizenDaysLeftToLive(newDaysLeftToLive)) ? newDaysLeftToLive : oldDaysLeftToLive;

            //update changes
            var update = Builders<BsonDocument>.Update
                .Set("Name", name)
                .Set("Salary", salary)
                .Set("HomeAddress", homeAddress)
                .Set("WorkAddress", workAddress)
                .Set("Age", age)
                .Set("DaysLeftToLive", daysLeftToLive);
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
        /// Delete one citizen from the collection.
        /// Author: Michael
        /// Date: 2017-10-15
        /// </summary>
        /// <param name="_id"></param>
        public async void DeleteOneCitizen(ObjectId _id) {
            var collection = Database.GetCollection<Citizens>("Citizens");
            var filter = Builders<Citizens>.Filter.Eq("_id", _id);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete one building from the collection by its id
        /// Author: Bill
        /// Date: 2017-10-31
        /// </summary>
        /// <param name="_id">building id</param>
        public async void DeleteOneBuilding(ObjectId _id)
        {
            var collection = Database.GetCollection<Buildings>("Buildings");
            var filter = Builders<Buildings>.Filter.Eq("_id", _id);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Delete all citizens (drop then recreate table)
        /// Author: Bill
        /// Date: 2017-10-31
        /// </summary>
        public void DeleteAllCitizens()
        {
            Database.DropCollection("Citizens");
            Database.CreateCollection("Citizens");
        }

        /// <summary>
        /// Delete all buildings (drop then recreate table)
        /// Author: Bill
        /// Date: 2017-10-31
        /// </summary>
        public void DeleteAllBuildings()
        {
            Database.DropCollection("Buildings");
            Database.CreateCollection("Buildings");
        }
    }
}
