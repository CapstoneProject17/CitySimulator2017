using DBInterface;
using Newtonsoft.Json;
using System;

namespace ServerForTheLogic.Json.LiteObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PersonTravel
    {
        [JsonProperty]
        public Guid Id { get;  set; }

        [JsonProperty]
        public Point Origin { get; set; }

        [JsonProperty]
        public Point Destination { get; set; }

        public PersonTravel(Guid Id, Point Origin, Point Destination)
        {
            this.Id = Id;
            this.Origin = Origin;
            this.Destination = Destination;
        }
    }
}
