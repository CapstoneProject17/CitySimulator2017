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
    /// Clock used for timing and synchronization
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Clock
    {
        public int NetMinutes { get; set; }
        public int NetHours { get; set; }
        public int NetDays { get; set; }
    }
}
