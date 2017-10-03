using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    class Building : Location
    {
        /// public int BuildSize { get; set; }
        /// <summary>
        /// the quality of the building 1-3, 1 being the lowest quality
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// number of people that live or work in this building
        /// </summary>
        public int Capacity { get; set; }

        public Building(string Name) : base(Name, false)
        { }
        public Building() : base()
        { }

    }


}
