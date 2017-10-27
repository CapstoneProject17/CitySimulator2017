using System;
using Newtonsoft.Json;
using System.Json;

namespace CitySimNetworkService
{
    internal class DatabaseHandler
    {

        internal JsonObject HandleRequest(DatabaseResourceRequest request)
        {
            
            switch (request.ResourceType)
            {
                case "building":
                    List<Building> obj = GetBuildings(request.ResourceID);
					break;
                case "person":
                    List<Citizen> obj = GetCitizens(request.ResourceID);
					break;
                default:
                    //FIX ME: JSON representation of invalid request
                    return new JsonObject();
            }

            jsonObj = JsonConvert.SerializeObject(obj);

			return jsonObj;
        }
    }
}