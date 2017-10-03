using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Road : Location
    {
        public Road() : base()
        {
            this.Type = "R";
        }
        public Road(string Name) : base(Name, true)
        {
            this.Type = "R";
        }
    }
}
