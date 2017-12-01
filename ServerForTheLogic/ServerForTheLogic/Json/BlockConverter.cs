using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DBInterface.Infrastructure;

namespace ServerForTheLogic.Json
{
    /// <summary>
    /// BlockConverter.
    /// For readin/writing Json to/from Blocks.
    /// <para/> Written by: Andrew Busto, 2017-11-26
    /// </summary>
    class BlockConverter : JsonConverter
    {
        /// <summary>
        /// Determines for the serializer if this converter should
        /// be used for an object of the given type.
        /// <para/> Written by: Andrew Busto, 2017-11-26 
        /// </summary>
        /// <param name="objectType"> The type of the object to convert. </param>
        /// <returns> True if object type is a Block, false otherwise. </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Block);
        }

        /// <summary>
        /// Determines for the serializer if this converter can write.
        /// It can.
        /// <para/> Written by: Andrew Busto, 2017-11-26
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Parses a section of Json (of the correct type)
        /// <para/> Written by: Andrew Busto, 2017-11-26
        /// </summary>
        /// <param name="reader"> The object reading the text. </param>
        /// <param name="objectType"> The type of the object being converted.  Unused. </param>
        /// <param name="existingValue"> The pre-existing value being parsed to.  Unused. </param>
        /// <param name="serializer"> The serializer object currently using this.  Unused. </param>
        /// <returns> A Block. </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Loads a JToken which should contain information corresponding to a Block.
            JToken jTok = JToken.Load(reader);
            Block output = null;

            // Checks to make sure the JToken loaded doesn't represent a null value.
            if (jTok.HasValues)
            {
                Guid guid = new Guid(jTok.Value<string>("Id"));

                // Checks if the Block has already been instantiated.
                // If it has, return the existing object.
                // Otherwise, instantiate a new Block.
                if (Block.blocks.ContainsKey(guid))
                {
                    output = Block.blocks[guid];
                }
                else
                {
                    output = jTok.ToObject<Block>();
                }
            }

            return output;
        }

        /// <summary>
        /// Writes a Block to a Json string.
        /// <para/> Written by: Andrew Busto, 2017-11-26
        /// </summary>
        /// <param name="writer"> The object writing the Json. </param>
        /// <param name="value"> The value to convert to Json. </param>
        /// <param name="serializer"> The serializer using this.  Unused. </param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            t.WriteTo(writer);
        }
    }
}
