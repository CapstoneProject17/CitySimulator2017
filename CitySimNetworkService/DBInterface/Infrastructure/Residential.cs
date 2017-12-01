using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInterface.Infrastructure
{
    /// <summary>
    /// Residential buildings hold a number of occupants, depending on size
    /// they currently have no functionality except for a place for humans to "sleep"
    /// for 8 simulated hours
    /// <para>Written by Connor Goudie, Andrew Busto 2017-10-02</para>
    /// <para>Last modified by Andrew Busto 2017-11-08</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Residential : Building
    {
        //max number of residents if building is tall
        public const int CAPACITY_TALL = 50;
        //max number of residents if building is not tall
        public const int CAPACITY_SHORT = 50;

        //current resident count
        [JsonProperty]
        public int NumberOfResidents { get; set; }

        /// <summary>
        /// Default constructor for residential buildings
        /// <para>Written by Connor Goudie 2017-10-02</para>
        /// </summary>
        public Residential() : base()
        {
            this.Type = "H";
        }

        /// <summary>
        /// Overloaded constructor for residential buildings
        /// <para>Written by Andrew Busto 2017-11-08</para>
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Residential(int capacity, Boolean isTall) : base("Residence", capacity, isTall)
        {
            this.Type = "H";
            if (isTall)
            {
                Capacity = CAPACITY_TALL;
            }
            else
            {
                Capacity = CAPACITY_SHORT;
            }
        }
    }
}
