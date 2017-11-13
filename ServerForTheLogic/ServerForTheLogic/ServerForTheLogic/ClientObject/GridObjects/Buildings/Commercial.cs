using ServerForTheLogic.ClientObject.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.ClientObject.Building

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
    class Commercial : Building
    {
        public Commercial(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity, int retailPrice, int inventoryCount)
            : base(guid, xPoint, yPoint, rating, isTall, capacity)
        {
            RetailPrice = retailPrice;
            InventoryCount = inventoryCount;
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
