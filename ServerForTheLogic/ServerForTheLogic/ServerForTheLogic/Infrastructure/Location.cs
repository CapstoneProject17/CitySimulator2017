using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    abstract class Location
    {
        //public Position Pos { get; set; }
        /// <summary>
        /// Name of the building (randomized company name from bogus library)
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Whether humans can walk on this location or not
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public bool Navigable { get; }
        /// <summary>
        /// Position of this cell in the world grid
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public Point Point { get; set; }
        /// <summary>
        /// Type of the building to display on console
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public string Type { get; set; }


        public Location(string Name, bool Navigable)
        {
            this.Name = Name;
            this.Navigable = Navigable;
        }
        public Location(string Name, bool Navigable, int x, int z)
        {
            this.Name = Name;
            this.Navigable = Navigable;
            Point = new Point(x, z);
        }

        public Location()
        {
        }

    }
}
