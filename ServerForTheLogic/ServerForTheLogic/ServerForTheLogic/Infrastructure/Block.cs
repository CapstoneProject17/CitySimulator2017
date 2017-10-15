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
        private static int ResCount;
        private static int ComCount;
        private static int IndCount;
        //width of the landplot array in terms of grid cells
        public const int BLOCK_WIDTH = 4;
        //height of the landplot array in terms of grid cells
        public const int BLOCK_LENGTH = 8;
        //starting point of the landplot in terms of world grid
        public Point StartPoint { get; }
        public List<Block> Adjacents { get; }
        //2d array of cells that contain locations
        public Location[,] LandPlot;
        //type of buildings a block will hold
        public BlockType Type { get; private set; }
        /// <summary>
        /// Constructs a new Block object using from the passed start point
        /// </summary>
        /// <param name="StartPoint"></param>
        public Block(Point StartPoint)
        {
            this.StartPoint = StartPoint;
            LandPlot = new Location[BLOCK_WIDTH, BLOCK_LENGTH];
            Adjacents = new List<Block>();
            Type = BlockType.Empty;
        }

        //if the current cell contains nothing, a new location object can be build in it.
        public bool canBuild(int x, int z)
        {
            return LandPlot[x, z] == null;
            //&&
            //(LandPlot[x - 1, z].Navigable ||
            //LandPlot[x + 1, z].GetType() == typeof(Road)||
            //LandPlot[x - 1, z - 1].GetType() == typeof(Road)||
            //LandPlot[x + 1, z + 1].GetType() == typeof(Road));
        }

        public void setBlockType()
        {
            if (ResCount < ComCount && ResCount < IndCount)
            {
                Type = BlockType.Residential;
                ResCount++;
            }
            else if (ComCount < ResCount && ComCount < IndCount)
            {
                Type = BlockType.Commercial;
                ComCount++;
            }
            else if (IndCount < ResCount && IndCount < ComCount)
            {
                Type = BlockType.Industrial;
                IndCount++;
            }
            else
            {
                Bogus.Randomizer rand = new Bogus.Randomizer();
                int x = rand.Number(2);
                if (x == 0)
                {
                    Type = BlockType.Residential;
                    ResCount++;
                }
                else if (x == 1)
                {
                    Type = BlockType.Industrial;
                    IndCount++;
                }
                else
                {
                    Type = BlockType.Commercial;
                    ComCount++;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString() + " " + StartPoint.ToString() + " "
                + Type.ToString() + " Res: " + ResCount + " Ind: " + IndCount + " Com: " + ComCount;
        }
    }
    public enum BlockType { Residential, Industrial, Commercial, Empty }
}
