using DBInterface.Econ;
using MongoDB.Bson;

namespace ServerForTheLogic.ClientObject
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Products being bought/sold within the simulated economy.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// 
    /// Update:
    /// 2017-11-12 Bill
    ///     - updated summary on all fields
    /// </summary>
    public class ProductDB
    {
        /// <summary>
        /// Constructor of a Product object that gets stored as a Product document in the database
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="globalCount">The total number of this product in the simulation</param>
        public ProductDB(string name, int globalCount)
        {
            Name = name;
            GlobalCount = globalCount;
        }

        public ProductDB(Product product)
        {
            Name = product.ProductName;
            GlobalCount = 0;
        }
        /// <summary>
        /// BSON objectId stored in the database.
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 1. Name can not be null
        /// 2. Name can not be empty
        /// 3. Name can not be longer than 30 letters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// GlobalCount int no limit
        /// </summary>
        public int GlobalCount { get; set; }
    }
}
