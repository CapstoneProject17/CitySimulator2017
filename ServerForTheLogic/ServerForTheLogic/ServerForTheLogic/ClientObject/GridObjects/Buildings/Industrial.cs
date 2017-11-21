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
    /// Industrial class for buildings which produce goods and have employees.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// 
    /// Update:
    /// 2017-11-12 Bill
    ///     - updated summary for all fields
    /// </summary>
    public class Industrial : Building
    {
        public Industrial(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity, int inventoryCount, int productionCost, int wholesalePrice)
            : base(guid, xPoint, yPoint, rating, isTall, capacity)
        {
            InventoryCount = inventoryCount;
            ProductionCost = productionCost;
            WholesalePrice = wholesalePrice;
        }

        /// <summary>
        /// InventoryCount - positive int only
        /// </summary>
        public int InventoryCount { get; set; }

        /// <summary>
        /// ProductionCost - positive int only
        /// </summary>
        public int ProductionCost { get; set; }

        /// <summary>
        /// WholesalePrice - positive int only
        /// </summary>
        public int WholesalePrice { get; set; }
    }
}
