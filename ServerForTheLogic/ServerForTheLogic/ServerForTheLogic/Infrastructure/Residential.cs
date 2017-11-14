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
        public Residential(int capacity,Boolean isTall) : base("Residence",capacity,isTall)
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
