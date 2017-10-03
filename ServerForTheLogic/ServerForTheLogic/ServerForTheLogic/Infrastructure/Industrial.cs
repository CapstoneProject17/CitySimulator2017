using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Industrial : Building
    {
        public Industrial() : base()
        {
            this.Type = "I";
        }
        public Industrial(string Name) : base(Name)
        {
            this.Type = "I";
        }
    }
}
