using Newtonsoft.Json;
using System;

namespace DALInterface.Infrastructure
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Building is the representation of a single building in the city, they have a 
    /// rating, which affects how it appears to clients, capacity which determines maximum
    /// workers/inhabitants, and istall, which also governs the model displayed, and affects
    /// the maximum number of occupants
    /// <para>Written by Chandu Dissanayake, Connor Goudie 2017-10-02</para>
    /// <para>Last modified by Connor Goudie 2017-11-14</para>
    /// </summary>
    public class Building : Location
    {
        [JsonProperty]
        // the quality of the building 1-3, 1 being the lowest quality
        public int Rating { get; set; }

        [JsonProperty]
        // number of people that live or work in this building
        public int Capacity { get; set; }

        [JsonProperty]
        // whether the building is larger or not
        public bool IsTall { get; private set; }

        /// <summary>
        /// Constructs a building using the passed in name
        /// </summary>
        /// <para>Written by Connor Goudie 2017-10-02</para>
        /// <param name="Name"></param>
        public Building(string Name) : base(Name)
        { }

        /// <summary>
        /// Constructs building using passed in capacity, name, and height
        /// </summary>
        /// <para>Written by Chandu Dissanayake 2017-11-13</para>
        /// <param name="Name"></param>
        /// <param name="Capacity"></param>
        /// <param name="isTall"></param>
        public Building(string Name, int Capacity, Boolean isTall) : base(Name)
        {
            this.Capacity = Capacity;
            this.IsTall = isTall;
        }

        /// <summary>
        /// Default constructor for building
        /// <para>Written by Connor Goudie 2017-10-02</para>
        /// <para>Last modified by Connor Goudie 2017-11-14</para>
        /// </summary>
        public Building() : base()
        { }

        public Building(Building building)
        {
            this.id = building.id;
            this.Name = building.Name;
            this.Point = building.Point;
            this.Type = building.Type;
            this.Rating = building.Rating;
            this.IsTall = building.IsTall;
        }
    }
}
