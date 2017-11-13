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
    /// Residential class for homes of inhabitants.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Residential : Building
    {
        public Residential(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity) : base(guid, xPoint, yPoint, rating, isTall, capacity)
        {

        }
    }
}
