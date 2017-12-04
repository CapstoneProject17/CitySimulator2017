﻿using DBInterface.Infrastructure;
using System;
namespace ServerForTheLogic.ClientObject.Buildings
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
    public class IndustrialDB : BuildingDB
    {
        public IndustrialDB(Industrial industrial) : base(industrial)
        {
        }
        public IndustrialDB(Guid guid, string name, int xPoint, int yPoint, int rating, int capacity, int inventoryCount, int productionCost, int wholesalePrice)
            : base(guid, name, xPoint, yPoint, rating, capacity)
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
