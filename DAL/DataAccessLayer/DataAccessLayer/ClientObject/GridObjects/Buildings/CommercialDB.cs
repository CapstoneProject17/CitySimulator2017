using DBInterface.Infrastructure;
using ServerForTheLogic.ClientObject.Buildings;
using System;
namespace ServerForTheLogic.ClientObject.Buildings

{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Commercial class represents buildings which buy goods from industrial providers and sell them to actors within the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// 
    /// Update:
    /// 2017-11-12 Bill
    ///     - updated summary for all fields
    /// </summary>
    public class CommercialDB : BuildingDB
    {
        public CommercialDB(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity, int retailPrice, int inventoryCount)
            : base(guid, xPoint, yPoint, rating, isTall, capacity)
        {
            RetailPrice = retailPrice;
            InventoryCount = inventoryCount;
        }


        public CommercialDB(Commercial commercial) : base(commercial)
        {

        }
        /// <summary>
        /// RetailPrice must be a valid integer
        /// </summary>
        public int RetailPrice { get; set; }

        /// <summary>
        /// InventoryCount must be a valid integer
        /// </summary>                  
        public int InventoryCount { get; set; }
    }
}
