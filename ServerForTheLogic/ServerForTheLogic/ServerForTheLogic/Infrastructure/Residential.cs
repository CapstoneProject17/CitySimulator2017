using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Residential : Building
    {
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
