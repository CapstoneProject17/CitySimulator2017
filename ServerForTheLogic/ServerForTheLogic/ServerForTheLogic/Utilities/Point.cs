using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Utilities
{
    struct Point
    {
        /// <summary>
        /// Basic position struct for buildings and roads
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public Point(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public int x { get; }
        public int z { get; }
    }
}
