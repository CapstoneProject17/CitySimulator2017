using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CitySimNetworkService
{
    /// <summary>
    /// <Module>Networking Server Connection</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Json Converter to handle converting into different types of requests</Description>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Initial creation of request coverter</ChangeLog>
    /// <Date>November 01, 2017</Date>
    /// </Author>
    /// </summary>
    public class RequestJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(BaseRequest).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject request = JObject.Load(reader);
            if (request["RequestType"].Value<string>() == "update")
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

        /// <summary>
        /// Writes to JSON (method stub needed for derived JsonConverter).
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
