using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// 
    /// </summary>
    class Building : Location
    {
        [JsonProperty]
        /// public int BuildSize { get; set; }
        /// <summary>
        /// the quality of the building 1-3, 1 being the lowest quality
        /// </summary>
        public int Rating { get; set; }
        [JsonProperty]
        /// <summary>
        /// number of people that live or work in this building
        /// </summary>
        public int Capacity { get; set; }

        [JsonProperty]
        public bool IsTall { get; private set; }
        
        public Building(string Name) : base(Name)
        { }

        public Building (string Name, int Capacity): base(Name)
        {
            this.Capacity = Capacity;
        }
        public Building() : base()
        { }

    }


}
