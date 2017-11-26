using System;
using Newtonsoft.Json;
using ServerForTheLogic.Infrastructure;
using Newtonsoft.Json.Linq;

namespace ServerForTheLogic.Json
{
    class LocationConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Location)) || objectType == typeof(Location);
        }

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jTok = JToken.Load(reader);
            Location output = null;

            if (jTok.HasValues)
            {
                string type = jTok.Value<string>("Type");
                Guid guid = new Guid(jTok.Value<string>("id"));

                if (Location.locations.ContainsKey(guid))
                {
                    output = Location.locations[guid];
                }
                else if (type.Equals("H"))
                {
                    output = jTok.ToObject<Residential>();
                }
                else if (type.Equals("C"))
                {
                    output = jTok.ToObject<Commercial>();
                }
                else if (type.Equals("I"))
                {
                    output = jTok.ToObject<Industrial>();
                }
                else if (type.Equals("R"))
                {
                    output = jTok.ToObject<Road>();
                }
            }

            return output;
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            t.WriteTo(writer);
        }
    }
}
