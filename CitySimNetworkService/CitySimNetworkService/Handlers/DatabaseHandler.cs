using System;
using Newtonsoft.Json;
using System.Json;
using System.Collections.Generic;

namespace CitySimNetworkService
{
    /// <summary> 
    /// Contains handler for database request. 
    /// </summary>
    /// 
    /// <author> 
    /// Harman Mahal 
    /// </author>
    internal class DatabaseHandler
    {
        /// <summary> 
        /// Handler; returns JSON object containing Buildings/Citizens information on valid request. 
        /// </summary>
        /// 
        /// <param name="request">
        /// Contains resource type and resource id. 
        /// </param>
        /// 
        /// <returns> 
        /// JSON object; has Buildings/Citizens/empty information. 
        /// </returns>
        internal JsonObject HandleRequest(DatabaseResourceRequest request)
        {
            JsonObject jsonObj;

            switch (request.ResourceType)
            {
                case "building":
                    List<Buildings> obj = GetBuildings(request.ResourceID);
                    jsonObj = JsonConvert.SerializeObject(obj);
			        return jsonObj;
                case "person":
                    List<Citizens> obj = GetCitizens(request.ResourceID);
                    jsonObj = JsonConvert.SerializeObject(obj);
                    return jsonObj;
                default:
                    //FIX ME: JSON representation of invalid request
                    return new JsonObject();
            }
        }
    }
}