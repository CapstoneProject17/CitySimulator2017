using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Block
    {
        //width of the landplot array in terms of grid cells
        public const int BLOCK_WIDTH = 4;
        //height of the landplot array in terms of grid cells
        public const int BLOCK_LENGTH = 8;
        //starting point of the landplot in terms of world grid
        public Point StartPoint { get; }
        //2d array of cells that contain locations
        public Location[,] LandPlot; 

        /// <summary>
        /// Constructs a new Block object using from the passed start point
        /// </summary>
        /// <param name="StartPoint"></param>
        public Block(Point StartPoint)
        {
            this.StartPoint = StartPoint;
            LandPlot = new Location[BLOCK_WIDTH, BLOCK_LENGTH];
        }

        //if the current cell contains nothing, a new location object can be build in it.
        public bool canBuild(int x, int z)
        {
            return LandPlot[x, z] == null; //&&
               //(LandPlot[x - 1, z].Navigable ||
                //LandPlot[x + 1, z].GetType() == typeof(Road)||
                //LandPlot[x - 1, z - 1].GetType() == typeof(Road)||
                //LandPlot[x + 1, z + 1].GetType() == typeof(Road));
        }

    }
}
