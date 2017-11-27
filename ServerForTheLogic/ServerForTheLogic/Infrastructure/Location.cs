using Newtonsoft.Json;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    /// <summary>
    /// Highest level class used in city infrastructure hierarchy, contains a name
    /// of the object, a point in which it exists, a type (used for console representation),
    /// and a unique ID
    /// <para>Written by Connor Goudie, Andrew Busto 2017-10-02</para>
    /// <para>Last modified by Andrew Busto 2017-11-08</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Location
    {
        public static Dictionary<Guid, Location> locations;

        [JsonProperty]
        //name of the building
        public string Name { get; set; }

        [JsonProperty]
        //location of the building within the grid
        public Point Point { get; set; }

        [JsonProperty]
        //type of building used for console representation
        public string Type { get; set; }

        [JsonProperty]
        //unique object id
        public Guid id { get; set; }

        static Location()
        {
            locations = new Dictionary<Guid, Location>();
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <para>Written by by Connor Goudie 2017-10-02</para>
        /// <para>Last modified by Andrew Busto 2017-10-19</para>
        /// <param name="Name"></param>
        public Location(string Name)
        {
            id = Guid.NewGuid();
            this.Name = Name;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <para>Written by by Connor Goudie 2017-10-02</para>
        /// <para>Last modified by Connor Goudie 2017-11-08</para>
        /// <param name="Name"></param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public Location(string Name, int x, int z)
        {
            id = Guid.NewGuid();
            this.Name = Name;
            Point = new Point(x, z);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <para>Written by Andrew Busto 2017-10-02</para>
        public Location()
        {
            id = Guid.NewGuid();
            locations.Add(id, this);
        }    
    }
}
