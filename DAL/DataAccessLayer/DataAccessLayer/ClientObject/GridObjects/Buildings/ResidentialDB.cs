using DBInterface.Infrastructure;
using System;

namespace ServerForTheLogic.ClientObject.Buildings
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Residential class for homes of inhabitants.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    public class ResidentialDB : BuildingDB
    {
        public ResidentialDB(Guid guid, int xPoint, int yPoint, int rating, bool isTall, int capacity) : base(guid, xPoint, yPoint, rating, isTall, capacity)
        {

        }

        public ResidentialDB(Residential residential) : base(residential)
        {

        }
    }
}
