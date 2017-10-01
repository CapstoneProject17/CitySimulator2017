using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Building : Location
    {
        //public int BuildSize { get; set; }
        public int Rating { get; set; }
        public int Capacity { get; set; }

        public Building(string Name) : base(Name, false)
        { }

    }


}
