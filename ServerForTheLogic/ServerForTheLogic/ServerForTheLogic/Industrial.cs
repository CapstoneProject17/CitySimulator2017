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
    /// Industrial class for buildings which produce goods and have employees.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Industrial
    {
        public Guid Guid { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        public int InventoryCount { get; set; }
        public int ProductionCost { get; set; }
        public int WholesalePrice { get; set; }
        public int Rating { get; set; }
        public int Capacity { get; set; }
        public Boolean IsTall { get; set; }
    }
}
