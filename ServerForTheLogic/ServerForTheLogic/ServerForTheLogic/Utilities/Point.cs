using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Utilities
{
    struct Point
    {
        public Point(int x, int z) : this()
        {
            this.x = x;
            this.z = z;
        }

        public int x { get; }
        public int z { get; }
    }
}
