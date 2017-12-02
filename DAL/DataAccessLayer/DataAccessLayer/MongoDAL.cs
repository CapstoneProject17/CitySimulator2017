using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServerForTheLogic;
using System;
using ServerForTheLogic.ClientObject;
using ServerForTheLogic.ClientObject.Buildings;
using DBInterface;
using DBInterface.Infrastructure;
using DBInterface.Econ;

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
    public class MongoDAL
	{

		private MongoClient client;

		/// <summary>
		/// MongoDAL creates MongoClient on instantiation, 
		/// DatabaseHandler creates MongoDAL on instantiation
		/// </summary>
		/// <author>Kevin</author>
		public MongoDAL()
		{
			client = new MongoClient();
		}

		/// <summary>
		/// Returns current database reference
		/// </summary>
		/// <author>Michael, Kevin</author>
		private IMongoDatabase Database
		{
			get
			{
				var database = client.GetDatabase("default");
				return database;
			}
		}

		/* ==================== Insert Section ==================== */

		/// <summary>
		/// Insert a new Person into the Database
		/// Author: 
		/// Updated: 
		/// 2017 11 12 Steph
		///     - Reflects schema change
		/// </summary>
		/// <param name="person">The person object being inserted into the database</param>
		/// <returns>Returns true if successful otherwise false</returns>
		public bool InsertPerson(Person person)
		{
            PersonDB personDB = new PersonDB(person);
			if (!DALValidator.DALPersonValidator(personDB))
			{
				return false;
			}
			else
			{
				var personCol = Database.GetCollection<PersonDB>("Person");
				personCol.InsertOne(personDB);
			}
			return true;
		}

		// Plural? Insert multiple citizens at once if needed...
		// Should it be a Task type with a return value?
		// this will only insert if all entries pass validation, not just the ones that do pass.
		// https://msdn.microsoft.com/en-us/library/dd235678#Remarks
		/// <summary>
		/// Inserts multiple people into the database
		/// Author: Michael
		/// Date: 2017-10-16
		/// Updated:
		/// 2017 11 12 Steph
		///     - Reflects schema change
		/// Date: 2017-11-12
		/// </summary>
		/// <param name="people">The list of people to be inserted into the database</param>
		/// <returns>Returns the collection if all the people are inserted otherwise null</returns>
		public Task InsertPeople(IEnumerable<Person> people)
        {
            List<PersonDB> peopleDB = new List<PersonDB>();
            foreach (Person p in people)
            {
                peopleDB.Add(new PersonDB(p));
            }
                foreach (PersonDB p in peopleDB)
			{
				if (!DALValidator.DALPersonValidator(p))
				{
					return null;
				}
			}
			var personCol = Database.GetCollection<PersonDB>("Person");
			return personCol.InsertManyAsync(peopleDB);
		}

		/// <summary>
		/// Insertion of one Residential Building Grid Object
		/// </summary>
		/// Author: Steph
		/// Date: 2017 11 10
		/// 
		/// Updated:
		/// 2017 11 10 Steph
		///     - reflects schema change
		/// <param name="residential">The residential building to be stored in the database</param>
		/// <returns>Returns true if stored in the database otherwise false</returns>
		public bool InsertResidential(Residential residential)
		{
            ResidentialDB residentialDB = new ResidentialDB(residential);
			if (!DALValidator.DALResidentialBuildingValidator(residentialDB))
			{
				return false;
			}
			else
			{
				var residentialCol = Database.GetCollection<ResidentialDB>("Residential");
				residentialCol.InsertOne(residentialDB);
			}
			return true;
		}

		/// <summary>
		/// Insertion of one Commercial Building Grid Object
		/// </summary>
		/// Author: Steph
		/// Date: 2017 11 10
		/// 
		/// Updated:
		/// 2017 11 10 Steph
		///     - reflects schema change
		/// <param name="commercial">The commercial building to be stored in the database</param>
		/// <returns>Returns true if stored in the database otherwise false</returns>
		public bool InsertCommercial(Commercial commercial)
		{
            CommercialDB commercialDB = new CommercialDB(commercial);
			if (!DALValidator.DALCommercialBuildingValidator(commercialDB))
			{
				return false;
			}
			else
			{
				var commercialCol = Database.GetCollection<CommercialDB>("Commercial");
				commercialCol.InsertOne(commercialDB);
			}
			return true;
		}

		/// <summary>
		/// Insertion of one Industrial Building Grid Object
		/// </summary>
		/// Author: Steph
		/// Date: 2017 11 10
		/// 
		/// Updated:
		/// 2017 11 10 Steph
		///     - reflects schema change
		/// <param name="industrial">The industrial building to be stored in the database</param>
		/// <returns>Returns true if stored in the database otherwise false</returns>
		public bool InsertIndustrial(Industrial industrial)
		{
            IndustrialDB industrialDB = new IndustrialDB(industrial);
			if (!DALValidator.DALIndustrialBuildingValidator(industrialDB))
			{
				return false;
			}
			else
			{
				var industrialCol = Database.GetCollection<IndustrialDB>("Industrial");
				industrialCol.InsertOne(industrialDB);
			}
			return true;
		}


		/// <summary>
		/// Author: Steph
		/// Date: 2017 11 10
		/// 
		/// Updated:
		/// 2017 11 12 Steph
		/// </summary>
		/// <param name="road">The road object being stored in the database</param>
		/// <returns>Returns true if stored in the database otherwise false</returns>
		public bool InsertRoad(Road road)
		{
            RoadDB roadDB= new RoadDB(road);
			if (!DALValidator.DALRoadValidator(roadDB))
			{
				return false;
			}
			else
			{
				var roadCol = Database.GetCollection<RoadDB>("Road");
				roadCol.InsertOne(roadDB);
			}
			return true;
		}

		/// <summary>
		/// Inserts one product
		/// Author: Steph
		/// Date: 2017 11 08
		/// </summary>
		/// <param name="product">Name of the product</param>
		/// <returns>Returns true if stored in the database otherwise false</returns>
		public bool InsertProduct(Product product)
		{
            ProductDB productDB= new ProductDB(product);
			if (!DALValidator.DALProductValidator(productDB))
			{
				return false;
			}
			else
			{
				var prodCol = Database.GetCollection<ProductDB>("Product");
				prodCol.InsertOne(productDB);
			}
			return true;
		}

		/// <summary>
		/// TODO: Inserts a savestate at a given timestamp.
		/// Author: Steph
		/// Date: 2017 11 10
		/// </summary>
		/// <param name="savestate">The savestate object being stored in the database</param>
		/// <returns>Return true if stored in the database otherwise false</returns>
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
		/// Date: 2017 11 09
		/// May or may not be used.
		/// </summary>
		/// <param name="clock">The clock object being stored in the database</param>
		/// <returns>True if succesful in storing object, otherwise false.</returns>
		public bool InsertClock(IClock clock)
		{
            ClockDB clockDB = new ClockDB(clock);
			if (!DALValidator.DALClockValidator(clockDB))
			{
				return false;
			}
			else
			{
				var clockCol = Database.GetCollection<ClockDB>("Clock");
				clockCol.InsertOne(clockDB);
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
		/// <param name="buildings">A list of the buildings to be inserted into the database</param>
		public Task InsertBuildings(IEnumerable<Building> buildings)
		{
            List<BuildingDB> buildingsDB = new List<BuildingDB>();
            foreach (Building b in buildings)
            {
                buildingsDB.Add(new BuildingDB(b));
            }
			foreach (BuildingDB b in buildingsDB)
			{
				if (!DALValidator.DALBuildingValidator(b))
				{
					System.Console.WriteLine("Building Id: " + b.Guid.ToString() + " did not meet validation rules.");
					return null;
				}
			}

			//TODO: we need to decide if inserting collection of buildings is needed
			var buildingCol = Database.GetCollection<Building>("Buildings");
			return buildingCol.InsertManyAsync(buildings);
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
		/// <param name="guid">The Guid of the object being retrieved from the database</param>
		/// <returns>The object based on the Guid otherwise returns null</returns>
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
		/// <returns>The clock from the database otherwise null</returns>
		public ClockDB GetClock()
		{
			var collection = Database.GetCollection<BsonDocument>("Clock");
			var clockData = collection.Find(new BsonDocument()).FirstOrDefault();
			if (clockData != null)
			{
				ClockDB myClock = BsonSerializer.Deserialize<ClockDB>(clockData.ToJson());
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
		/// <param name="name">The name of the product being retrieved from the database</param>
		/// <returns>The product on success otherwise null</returns>
		public ProductDB GetProduct(string name)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
			var collection = Database.GetCollection<BsonDocument>("Product");
			var product = collection.Find(filter).First();
			if (product != null)
			{
				ProductDB myProduct = BsonSerializer.Deserialize<ProductDB>(product.ToJson());
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
		/// <returns>Returns the SaveState if successful otherwise null</returns>
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
		/// <para>OLD DESCRIPTION! READ THE LATEST UPDATE FOR HOW TO USE THIS METHOD!</para>
		/// <para>
		/// update one citizen by its Id. 
		/// if string field is empty string then there will be no change in the original string,
		/// if int field == -1 then there will be no change in the original int value.
		/// updated value also have to meet validation rules, otherwise there's no change to the old value.
		/// </para> 
		/// <para>
		/// Author: Bill
		/// Date: 2017-10-31
		/// </para>
		/// <para>
		/// Updated: Micahel
		/// Date: 2017-11-13 
		/// Updated the method to use the new DAL convention. Uses PersonValidator.
		/// </para>
		/// <para>
		/// Updated: Bill
		/// Date: 2017-11-13
		/// </para>
		/// <para>LATEST VERSION</para>
		/// <para>
		/// Removed deserilizing The BSON and validating each parameter individually 
		/// and replacing the old fields with the new one. Now the method creates a new Person object and validates the
		/// new Person object. It then updates the Person document in the database. If the field is not being updated
		/// then it must still be included even if it is not changed.
		/// </para>
		/// </summary>
		/// <param name="guid">The GUID of the person that is being updated</param>
		/// <param name="firstName">The first name of the person. Can be new if being updated or remain as the old one</param>
		/// <param name="lastName">The last name of the person. Can be new if being updated or remain as the old one</param>
		/// <param name="monthlyIncome">The monthly income of the person</param>
		/// <param name="accountBalance">The monetary balance in the person's bank account</param>
		/// <param name="workplaceID">The GUID of the person's work place</param>
		/// <param name="workplaceX">The X coordinate of the person's work place</param>
		/// <param name="workplaceY">The Y coordinate of the person's work place</param>
		/// <param name="homeID">The GUID of the person's home</param>
		/// <param name="homeX">The X coordinate of the person's home</param>
		/// <param name="homeY">The Y coordinate of the person's home</param>
		/// <param name="daysLeft">The number of days left the person has to live</param>
		/// <param name="age">The current age of the person</param>
		/// <param name="startShift">The starting time of when the person's work begins</param>
		/// <param name="endShift">The ending time of when the person's work ends</param>
		public async void UpdatePerson(Person person)
		{
            //michael
			var collection = Database.GetCollection<BsonDocument>("Person");
			var filter = Builders<BsonDocument>.Filter.Eq("guid", person.Id);
			var personListData = await collection.Find(filter).ToListAsync();
			if (personListData == null || personListData.Count == 0)
			{
				Console.WriteLine("Can not update person, guid is invalid.");
				return;
			}

			PersonDB personToUpdate = new PersonDB(person);
			if (!DALValidator.DALPersonValidator(personToUpdate))
			{
				Console.WriteLine("Can not update person, at least one of the input field is invalid.");
				return;
			}

			var update = Builders<BsonDocument>.Update
				.Set("guid", person.Id)
				.Set("FirstName", person.FName)
				.Set("LastName", person.LName)
				.Set("MonthlyIncome", person.MonthlyIncome)
				.Set("AccountBalance", person.Funds)
				.Set("WorkplaceID", person.Workplace.Id)
				.Set("WorkplaceX", person.Workplace.Point.X)
				.Set("WorkplaceY", person.Workplace.Point.Z)
				.Set("HomeID", person.Home.Id)
				.Set("HomeX", person.Home.Point.X)
				.Set("HomeY", person.Home.Point.Z)
				.Set("DaysLeft", person.DaysLeft)
				.Set("Age", person.Age)
				.Set("StartShift", person.StartShift)
				.Set("EndShift", person.EndShift);
			var result = await collection.UpdateOneAsync(filter, update);
		}

		/// <summary>
		/// Update one residential building by guid
		/// 
		/// Author: Bill
		/// Date: 2017-11-13
		/// </summary>
		/// <param name="guid">The GUID of the Residential building</param>
		/// <param name="xPoint">The X coordinate of the Residential building</param>
		/// <param name="yPoint">The Y coordinate of the Residential building</param>
		/// <param name="rating">The rating of the Residential building</param>
		/// <param name="isTall">Boolean if the building is tall or not</param>
		/// <param name="capacity">The capacity of the Residential building</param>

		public async void UpdateResidentialBuilding(Residential residential)
		{
            //michael got this
			var collection = Database.GetCollection<BsonDocument>("Residential");
			var filter = Builders<BsonDocument>.Filter.Eq("guid", residential.Id);
			var residentialListData = await collection.Find(filter).ToListAsync();
			if (residentialListData == null || residentialListData.Count == 0)
			{
				Console.WriteLine("Can not update residential building, guid is invalid.");
				return;
			}

			ResidentialDB residentialBuildingToUpdate = new ResidentialDB(residential);

			if (!DALValidator.DALResidentialBuildingValidator(residentialBuildingToUpdate))
			{
				Console.WriteLine("Can not update residential building, at least one of the input field is invalid.");
				return;
			}

			var update = Builders<BsonDocument>.Update
				.Set("guid", residential.Id)
				.Set("XPoint", residential.Point.X)
				.Set("YPoint", residential.Point.Z)
				.Set("Rating", residential.Rating)
				.Set("IsTall", residential.IsTall)
				.Set("Capacity", residential.Capacity);
			var result = await collection.UpdateOneAsync(filter, update);
		}

        /// <summary>
        /// Update one commercial building by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid">The GUID of the Commercial building</param>
        /// <param name="xPoint">The X coordinate of the Commercial building</param>
        /// <param name="yPoint">The Y coordinate of the Commercial building</param>
        /// <param name="rating">The rating of the Commercial building</param>
        /// <param name="isTall">Boolean if the building is tall or not</param>
        /// <param name="capacity">The capacity of the Commercial building</param>
        /// <param name="retailPrice">The retail price of the Commercial building</param>
        /// <param name="inventoryCount">The inventory count of the Commercial building</param>
    public async void UpdateCommercialBuilding(Commercial commercial)
		{
			var collection = Database.GetCollection<BsonDocument>("Commercial");
			var filter = Builders<BsonDocument>.Filter.Eq("guid", commercial.Id);
			var commercialListData = await collection.Find(filter).ToListAsync();
			if (commercialListData == null || commercialListData.Count == 0)
			{
				Console.WriteLine("Can not update commercial building, guid is invalid.");
				return;
			}

			CommercialDB commercialBuildingToUpdate = new CommercialDB(commercial);

			if (!DALValidator.DALCommercialBuildingValidator(commercialBuildingToUpdate))
			{
				Console.WriteLine("Can not update commercial building, at least one of the input field is invalid.");
				return;
			}
            //TODO: fix retail price and inventory count on commercial building
			var update = Builders<BsonDocument>.Update
				.Set("guid", commercial.Id)
				.Set("XPoint", commercial.Point.X)
				.Set("YPoint", commercial.Point.Z)
				.Set("Rating", commercial.Rating)
				.Set("IsTall", commercial.IsTall)
				.Set("Capacity", commercial.Capacity)
				.Set("RetailPrice", 0)
				.Set("InventoryCount", 0);
			var result = await collection.UpdateOneAsync(filter, update);
		}

        /// <summary>
        /// Update one industrial building by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid">The GUID of the industrial building being updated</param>
        /// <param name="xPoint">The X coordinate of the industrial building being updated</param>
        /// <param name="yPoint">The Y coordinate of the industrial building being updated</param>
        /// <param name="rating">The rating of the industrial building being updated</param>
        /// <param name="isTall">Boolean if the building is tall or not</param>
        /// <param name="capacity">The employee capacity of the industrial building being updated</param>
        /// <param name="inventoryCount">The inventory count of the industrial building being updated</param>
        /// <param name="productionCost">The cost it takes the building to produce one product</param>
        /// <param name="wholesalePrice">The price the building sells its product to another building</param>
        public async void UpdateIndustrialBuilding(Industrial industrial)
        {
            var collection = Database.GetCollection<BsonDocument>("Industrial");
            var filter = Builders<BsonDocument>.Filter.Eq("guid", industrial.Id);
            var commercialListData = await collection.Find(filter).ToListAsync();
            if (commercialListData == null || commercialListData.Count == 0)
            {
                Console.WriteLine("Can not update commercial building, guid is invalid.");
                return;
            }

            IndustrialDB industrialBuildingToUpdate = new IndustrialDB(industrial);
            if (!DALValidator.DALIndustrialBuildingValidator(industrialBuildingToUpdate))
            {
                Console.WriteLine("Can not update commercial building, at least one of the input field is invalid.");
                return;
            }
            //TODO: fix retail price and inventory count on industrial building
            var update = Builders<BsonDocument>.Update
                .Set("guid", industrial.Id)
                .Set("XPoint", industrial.Point.X)
                .Set("YPoint", industrial.Point.Z)
                .Set("Rating", industrial.Rating)
                .Set("IsTall", industrial.IsTall)
                .Set("Capacity", industrial.Capacity)
                .Set("RetailPrice", 0)
                .Set("InventoryCount", 0);
            var result = await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Update one road by guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-13
        /// </summary>
        /// <param name="guid">The GUID of the road object being updated</param>
        /// <param name="xPoint">The X coordinate of the road being updated</param>
        /// <param name="yPoint">The Y coordinate of the road being updated</param>
      public async void UpdateRoad(Road road)
		  {
          var collection = Database.GetCollection<BsonDocument>("Road");
          var filter = Builders<BsonDocument>.Filter.Eq("guid", road.Id);
          var roadListData = await collection.Find(filter).ToListAsync();
          if (roadListData == null || roadListData.Count == 0)
          {
            Console.WriteLine("Can not update road, guid is invalid.");
            return;
          }

          RoadDB roadToUpdate = new RoadDB(road);
          if (!DALValidator.DALRoadValidator(roadToUpdate))
          {
            Console.WriteLine("Can not update road, at least one of the input field is invalid.");
            return;
          }

          var update = Builders<BsonDocument>.Update
            .Set("guid", road.Id)
            .Set("XPoint", road.Point.X)
            .Set("YPoint", road.Point.Z);
          var result = await collection.UpdateOneAsync(filter, update);
		    }

		/// <summary>
		/// Update clock
		/// 
		/// Author: Bill
		/// Date: 2017-11-13
		/// </summary>
		/// <param name="minutes">The minutes of the clock</param>
		/// <param name="hours">The hours of the clock</param>
		/// <param name="days">The days of the clock</param>
		/// <param name="years">The years of the clock</param>
		public async void UpdateClock(IClock clock)
		{
			var collection = Database.GetCollection<BsonDocument>("Clock");
			var clockData = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
			if (clockData == null)
			{
				Console.WriteLine("clock collection is empty.");
				return;
			}
			var filter = Builders<BsonDocument>.Filter.Eq("_id", clockData["_id"].ToString());

			ClockDB clockToUpdate = new ClockDB(clock);
			if (!DALValidator.DALClockValidator(clockToUpdate))
			{
				Console.WriteLine("Can not update clock, at least one of the input field is invalid.");
				return;
			}
			var update = Builders<BsonDocument>.Update
							.Set("NetMinutes", clock.NetMinutes)
							.Set("NetHours", clock.NetHours)
							.Set("NetDays", clock.NetDays)
							.Set("NetYears", clock.NetYears);
			var result = await collection.UpdateOneAsync(filter, update);
		}

		/// <summary>
		/// update product by product name
		/// 
		/// Author: Bill
		/// Date: 2017-11-13
		/// </summary>
		/// <param name="name">The name of the product to be updated</param>
		/// <param name="globalCount">The global count of the product</param>
		public async void UpdateProduct(Product product)
		{
			var collection = Database.GetCollection<BsonDocument>("Product");
			var filter = Builders<BsonDocument>.Filter.Eq("Name", product.ProductName);
			var roadListData = await collection.Find(filter).ToListAsync();
			if (roadListData == null || roadListData.Count == 0)
			{
				Console.WriteLine("Can not update product, name is invalid.");
				return;
			}

			ProductDB productToUpdate = new ProductDB(product);
			if (!DALValidator.DALProductValidator(productToUpdate))
			{
				Console.WriteLine("Can not update product, at least one of the input field is invalid.");
				return;
			}
            //TODO: fix this so products have a global count
			var update = Builders<BsonDocument>.Update
				.Set("Name", product.ProductName)
				.Set("GlobalCount", 0);
			var result = await collection.UpdateOneAsync(filter, update);
		}

		/// <summary>
		/// update SaveState by objectId
		/// 
		/// Author: Michael
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
		/// Updated: 
		/// 2017-10-15 Steph
		///     - reflects schema changes
		/// </summary>
		/// <param name="guid">The GUID of the person being deleted from the DB</param>
		public async void DeleteOnePerson(Person person)
		{
			var collection = Database.GetCollection<PersonDB>("Person");
			//what to do here?
			var filter = Builders<PersonDB>.Filter.Eq("guid", person.Id);
			await collection.DeleteOneAsync(filter);
		}

		/// <summary>
		/// Delete one building from the collection by its Id
		/// Author: Bill
		/// 
		/// Updated: 
		/// 2017-10-31 Steph
		///     - reflects schema changes 
		/// Updated: 
		/// 2017-11-14 Michael
		///  - Fixed typo. Method took in wrong parameter.
		/// </summary>
		/// <param name="residential">The Residential building object to be deleted</param>
		/// <param name="guid">The GUID of the residential building to be deleted</param>
		public async void DeleteOneResidential(Residential residential)
		{
			var collection = Database.GetCollection<ResidentialDB>("Residential");
			var filter = Builders<ResidentialDB>.Filter.Eq("guid", residential.Id);
			await collection.DeleteOneAsync(filter);
		}

		/// <summary>
		/// Delete one building from the collection by its Id
		/// Author: Bill
		/// Updated: Steph - reflects schema changes
		/// Date: 2017-10-31
		/// Updated: Michael - Fixed typo. Method took in wrong parameter.
		/// Date: 2017-11-14
		/// </summary>
		/// <param name="commercial">The Commercial building object to be deleted</param>
		/// <param name="guid">The GUID of the Commercial buidling object being deleted</param>
		public async void DeleteOneCommercial(Commercial commercial)
		{
			var collection = Database.GetCollection<CommercialDB>("Commercial");
			var filter = Builders<CommercialDB>.Filter.Eq("guid", commercial.Id);
			await collection.DeleteOneAsync(filter);
		}

		/// <summary>
		/// Delete one building from the collection by its Id
		/// Author: Bill
		/// Date:
		/// 
		/// Updated: 
		/// Steph - reflects schema changes
		///     Date: 2017-10-31
		/// Michael - Fixed typo. Method took in wrong parameter.
		///     Date: 2017-11-14
		/// </summary>
		/// <param name="industrial">The Industrial building object to be deleted</param>
		/// <param name="guid">The GUID of the Industrial building being deleted</param>
		public async void DeleteOneIndustrial(Industrial industrial)
		{
			var collection = Database.GetCollection<IndustrialDB>("Industrial");
			var filter = Builders<IndustrialDB>.Filter.Eq("guid", industrial.Id);
			await collection.DeleteOneAsync(filter);
		}

		/// <summary>
		/// Delete all citizens (drop then recreate table)
		/// Author: Bill
		/// Updated: 
		/// Steph - reflects schema changes
		///     Date: 2017-10-31
		/// </summary>
		public void DeleteAllCitizens()
		{
			Database.DropCollection("Person");
			Database.CreateCollection("Person");
		}

		/// <summary>
		/// Delete all residential type buildings (drop then recreate table)
		/// Author: Steph
		///     Date: 2017-11-13
		/// </summary>
		public void DeleteAllResidential()
		{
			Database.DropCollection("Residential");
			Database.CreateCollection("Residential");
		}

		/// <summary>
		/// Delete all commercial type buildings (drop then recreate table)
		/// Author: Steph
		///     Date: 2017-11-13
		/// </summary>
		public void DeleteAllCommercial()
		{
			Database.DropCollection("Commercial");
			Database.CreateCollection("Commercial");
		}

		/// <summary>
		/// Delete all industrial type buildings (drop then recreate table)
		/// Author: Steph
		///     Date: 2017-11-13
		/// </summary>
		public void DeleteAllIndustrial()
		{
			Database.DropCollection("Industrial");
			Database.CreateCollection("Industrial");
		}

		/// <summary>
		/// Delete all roads (drop then recreate table)
		/// Author: Steph
		///     Date: 2017-11-13
		/// </summary>
		public void DeleteAllRoads()
		{
			Database.DropCollection("Road");
			Database.CreateCollection("Road");
		}

		/// <summary>
		/// Delete all products (drop then recreate table)
		/// Author: Steph
		///     Date: 2017-11-13
		/// </summary>
		public void DeleteAllProducts()
		{
			Database.DropCollection("Product");
			Database.CreateCollection("Product");
		}
	}
}
