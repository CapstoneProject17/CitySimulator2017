using DBInterface.Infrastructure;
using ServerForTheLogic.ClientObject;
using System;

namespace ServerForTheLogic
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Road class mapped to Road collection of the database and the "roads" in the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    public class RoadDB : GridObject
    {
        /// <summary>
        /// The Road constructor. Calls the base GridObject constructor
        /// </summary>
        /// <param name="guid">Guid of the road</param>
        /// <param name="xPoint">X coordinate of the road</param>
        /// <param name="yPoint">Y coordinate of the road</param>
        public RoadDB(Guid guid, int xPoint, int yPoint) : base(guid, xPoint, yPoint)
        {
        }

        public RoadDB(Road road) : base(road)
        {

        }
    }
}
