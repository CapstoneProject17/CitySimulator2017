using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Residential : Building
    {
        public Residential() : base()
        {
            this.Type = "H";
        }
        public Residential(string Name) : base(Name)
        {

            this.Type = "H";
        }
    }
}
