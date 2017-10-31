using System;
using Newtonsoft.Json;
using System.Json;
using System.Collections.Generic;

namespace CitySimNetworkService
{
    internal class DatabaseHandler
    {

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