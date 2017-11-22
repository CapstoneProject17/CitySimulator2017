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
    /// Road class mapped to Road collection of the database and the "roads" in the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Road
    {
        public Guid Guid { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
    }
}
