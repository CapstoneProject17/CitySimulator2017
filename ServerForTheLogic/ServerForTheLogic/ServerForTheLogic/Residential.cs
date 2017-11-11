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
    /// Residential class for homes of inhabitants.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Residential
    {
        public int XPoint { get; set; }
        public Guid Guid { get; set; }
        public int Capacity { get; set; }
        public int Rating { get; set; }
        public Boolean IsTall { get; set; }
    }
}
