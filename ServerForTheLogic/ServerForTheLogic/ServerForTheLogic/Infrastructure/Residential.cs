using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    [JsonObject(MemberSerialization.OptIn)]
    class Residential : Building
    {
        public const int CAPACITY_TALL = 100;
        public const int CAPACITY_SHORT = 10;

        [JsonProperty]
        public int NumberOfResidents { get; set; }

        public Residential() : base()
        {
            this.Type = "H";
        }
        public Residential(int capacity) : base("Residence",capacity)
        {

            this.Type = "H";
        }
    }
}
