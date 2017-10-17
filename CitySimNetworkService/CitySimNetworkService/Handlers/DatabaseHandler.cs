using System;
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
                    Building obj = GetBuildings(request.ResourceID);
					break;
                case "person":
                    Citizen obj = GetCitizens(request.ResourceID);
					break;
                case "road":
                    Road obj = GetRoad(request.ResourceID);
					break;
                default:
                    return new JsonObject();
            }

            jsonObj = JsonConvert.SerializeObject(obj);

			return jsonObj;
            //throw new NotImplementedException();
        }
    }
}