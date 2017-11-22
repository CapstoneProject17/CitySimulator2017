using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Commercial class represents buildings which buy goods from industrial providers and sell them to actors within the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Commercial
    {
        public Guid Guid { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        public int Rating { get; set; }
        public int RetailPrice { get; set; }
        public int Capacity { get; set; }
        public Boolean IsTall { get; set; }
        public int InventoryCount { get; set; }
    }
}
