using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitySimNetworkService
{
    // class set public for testing
    public class RequestJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(BaseRequest).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject request = JObject.Load(reader);
            if (request["type"].Value<string>() == "update")
            {
                if (request["FullUpdate"].Value<bool>())
                {
                    return request.ToObject<SimulationUpdateRequest>();
                }
                else
                {
                    return request.ToObject<PartialSimulationUpdateRequest>();
                }

            }
            else
            {
                return request.ToObject<DatabaseResourceRequest>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
