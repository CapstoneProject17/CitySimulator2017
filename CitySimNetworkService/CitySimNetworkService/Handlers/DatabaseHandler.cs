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
                //Call the function to get building info
                case "person":
                //call the function to get person info
                default:
                    return new JsonObject();
            }

            throw new NotImplementedException();
        }
    }
}