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
        public string Name { get; set; }
        public bool Navigable { get; }
        public Point Point { get;}


        public Location(string Name, bool Navigable)
        {
            this.Name = Name;
            this.Navigable = Navigable;
        }
        public Location(string Name, bool Navigable, int x, int z)
        {
            this.Name = Name;
            this.Navigable = Navigable;
            Point = new Point(x,z);
           
        }
        
    }
}
