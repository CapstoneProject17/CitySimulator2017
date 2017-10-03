using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Commercial : Building
    {
        public Commercial() : base()
        {
            this.Type = "C";
        }
        public Commercial(string Name) : base(Name)
        {
            this.Type = "C";
        }
    }
}
