using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace DataAccessLayer {

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
    class MongoDAL {
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
            var citizensCol = Database.GetCollection<Citizens>("Citizens");
            citizensCol.InsertOne(citizen);
        }

        // Plural? Insert multiple citizens at once if needed...
        // Should it be a Task type with a return value?
        // https://msdn.microsoft.com/en-us/library/dd235678#Remarks
        public Task InsertCitizens(IEnumerable<Citizens> citizens) {
            var citizensCol = Database.GetCollection<Citizens>("Citizens");
            return citizensCol.InsertManyAsync(citizens);
        }

        public void InsertBuilding(Buildings building) {
            var buildingCol = Database.GetCollection<Buildings>("Buildings");
            buildingCol.InsertOne(building);
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
    }
}
