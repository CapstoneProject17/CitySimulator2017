using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CitySimNetworkService
{
    /// <summary> 
    /// Contains handler for database request. 
    /// </summary>
    /// 
    /// <author> 
    /// Harman Mahal 
    /// </author>
    public class DatabaseHandler
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
        internal string HandleRequest(DatabaseResourceRequest request)
        {

            switch (request.ResourceType)
            {
                case "building":
                    List<Buildings> obj = GetBuildings(request.ResourceID);
                    return JsonConvert.SerializeObject(obj);
                case "person":
                    List<Citizens> citizenObj = GetCitizens(request.ResourceID);
                    return JsonConvert.SerializeObject(citizenObj);
                default:
                    //FIX ME: JSON representation of invalid request
                    return "";
            }
        }


        //Stub method due to removing DAL integration
        private List<Citizens> GetCitizens(string resourceID)
        {
            throw new NotImplementedException();
        }

        //Stub method due to removing DAL integration
        private List<Buildings> GetBuildings(string resourceID)
        {
            throw new NotImplementedException();
        }
    }
}