using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DBInterface.Infrastructure;

namespace ServerForTheLogic.Json
{
    /// <summary>
    /// LocationConverter.
    /// For reading/writing Json to/from Locations.
    /// PRESENTLY UNUSED AS THE SAVING FUNCTION WAS REMOVED.
    /// <para/> Written by: Andrew Busto, 2017-11-25
    /// </summary>
    class LocationConverter : JsonConverter
    {
        /// <summary>
        /// Determines for the serializer if this converter should
        /// be used for an object of the given type.
        /// <para/> Written by: Andrew Busto, 2017-11-25 
        /// </summary>
        /// <param name="objectType"> The type of the object to convert. </param>
        /// <returns> True if object type is a Location, false otherwise. </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Location)) || objectType == typeof(Location);
        }

        /// <summary>
        /// Determines for the serializer if this converter can write.
        /// It can.
        /// <para/> Written by: Andrew Busto, 2017-11-25
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Parses a section of Json (of the correct type)
        /// <para/> Written by: Andrew Busto, 2017-11-25
        /// </summary>
        /// <param name="reader"> The object reading the text. </param>
        /// <param name="objectType"> The type of the object being converted.  Unused. </param>
        /// <param name="existingValue"> The pre-existing value being parsed to.  Unused. </param>
        /// <param name="serializer"> The serializer object currently using this.  Unused. </param>
        /// <returns> A Location of the appropriate type. </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Loads a JToken which should contain information corresponding to a Location.
            JToken jTok = JToken.Load(reader);
            Location output = null;

            // Checks to make sure the JToken loaded doesn't represent a null value.
            if (jTok.HasValues)
            {
                string type = jTok.Value<string>("Type");
                Guid guid = new Guid(jTok.Value<string>("Id"));
                
                // Checks if the location has already been instantiated.
                // If it has, return the existing object.
                // Otherwise, instantiate an object based on it's "type."
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
                    Console.WriteLine(output.Point);
                }
            }

            return output;
        }

        /// <summary>
        /// Writes a Location to a Json string.
        /// <para/> Written by: Andrew Busto, 2017-11-25
        /// </summary>
        /// <param name="writer"> The object writing the Json. </param>
        /// <param name="value"> The value to convert to Json. </param>
        /// <param name="serializer"> The serializer using this.  Unused. </param>
        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            t.WriteTo(writer);
        }
    }
}
