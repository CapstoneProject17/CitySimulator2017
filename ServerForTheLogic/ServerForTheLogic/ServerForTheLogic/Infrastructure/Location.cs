using Newtonsoft.Json;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class Location
    {
        [JsonProperty]
        //public Position Pos { get; set; }
        /// <summary>
        /// Name of the building (randomized company name from bogus library)
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public string Name { get; set; }

        [JsonProperty]
        /// <summary>
        /// Position of this cell in the world grid
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public Point Point { get; set; }
        [JsonProperty]
        /// <summary>
        /// Type of the building to display on console
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public string Type { get; set; }

        [JsonProperty]
        /// <summary>
        /// 
        /// </summary>
        public Guid id { get; set; }


        public Location(string Name)
        {
            id = Guid.NewGuid();
            this.Name = Name;
        }
        public Location(string Name, int x, int z)
        {
            id = Guid.NewGuid();
            this.Name = Name;
            Point = new Point(x, z);
        }

        public Location()
        {
            id = Guid.NewGuid();
        }

    }
}
