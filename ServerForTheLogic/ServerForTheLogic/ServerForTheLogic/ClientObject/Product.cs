using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Product
    {

        public Product(string name, int globalCount)
        {
            Name = name;
            GlobalCount = globalCount;
        }
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
