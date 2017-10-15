using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class City
    {
        /// <summary>
        /// Max width of the city grid
        /// </summary>
        public const int CITY_WIDTH = 100;
        /// <summary>
        /// Max length of the city grid
        /// </summary>
        public const int CITY_LENGTH = 200;

        /// <summary>
        /// List of all homes in the city
        /// </summary>
        public List<Residential> homes { get; set; }
        /// <summary>
        /// List of all places of work in the city
        /// </summary>
        public List<Building> workplaces { get; set; }
        /// <summary>
        /// List of all inhabitants of the city
        /// </summary>
        public List<Person> AllPeople { get; set; }
        /// <summary>
        /// The grid that all buildings/roads/people exist in
        /// </summary>
        public Location[,] map { get; set; }
        /// <summary>
        /// 2D array of city blocks, this is for easier city expansion
        /// NOTE: not currently implemented
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public Block[,] blockMap { get; set; }

        /// <summary>
        /// Clock to keep track of the time that 
        /// has passed since city creation.
        /// </summary>
        private Clock clock;

        /// <summary>
        /// Constructor for a new city, creates the 100x200 grid of cells,
        /// and the grid of city blocks
        /// <para/> Last edited:  2017-10-02
        /// </summary>
        public City()
        {
            map = new Location[CITY_WIDTH, CITY_LENGTH];
            blockMap = new Block[CITY_WIDTH / Block.BLOCK_WIDTH, CITY_LENGTH / Block.BLOCK_LENGTH];
            AllPeople = new List<Person>();
            homes = new List<Residential>();
            workplaces = new List<Building>();

            int width = CITY_WIDTH / Block.BLOCK_WIDTH;
            int height = CITY_LENGTH / Block.BLOCK_LENGTH;

            //fills the blockMap array
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    blockMap[i, j] = new Block(new Point(i * (Block.BLOCK_WIDTH - 1), j * (Block.BLOCK_LENGTH - 1)));
                }
            }
            // clock = new Clock();

            // TO DO: code to create initial state, or load from DB

            // blocks.Add(new Block(new Point(CITY_WIDTH / 2, CITY_LENGTH / 2)));
        }

        /// <summary>
        /// Returns the location object at the specified point
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Location GetLocationAt(Point p)
        {
            return map[p.x, p.z];
        }
        /// <summary>
        /// Returns the location object at the specified x z coordinate 
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Location GetLocationAt(int x, int z)
        {
            return map[x, z];
        }

        /// <summary>
        /// Adds references to a specified block's adjacent blocks.
        /// </summary>
        /// <param name="b"></param>
        public void setAdjacents(Block b)
        {
            int x = b.StartPoint.x / (Block.BLOCK_WIDTH - 1);
            int z = b.StartPoint.z / (Block.BLOCK_LENGTH - 1);
            for (int i = x - 1; i < x + 2; ++i)
            {
                //if out of bounds of the map, skip
                if (i < 0 || (i + x) >= blockMap.GetLength(0))
                    continue;
                for (int j = z - 1; j < z + 2; ++j)
                {
                    //if out of bounds of the map, or on the current block's cell, skip
                    if (j < 0 || (j + z) >= blockMap.GetLength(1) || (j == z && i == x))
                        continue;
                    //checks if adjacent block is null (though it should never be null)
                    if (blockMap[i + x, j + z] != null)
                    {
                        blockMap[x, z].Adjacents.Add(blockMap[i + x, j + z]);
                    }
                    else
                    {
                        blockMap[x, z].Adjacents.Add(null);
                    }
                }
            }

            /*       FOR DEBUGGING
             * for (int i = 0; i < blockMap.GetLength(0); i++)
            {
                for (int j = 0; j < blockMap.GetLength(1); j++)
                    if (blockMap[i, j] == null)
                        Console.Write(".");
                    else
                        Console.Write("B");
                Console.WriteLine();
            }

            Block temp = blockMap[x, z];
            for (int i = 0; i < temp.Adjacents.Count; i++)
            {
                if (temp.Adjacents[i] == null)
                    Console.WriteLine("empty");
                else
                    Console.WriteLine(temp.Adjacents[i]);
            }*/
        }

        public void expandCity()
        {
            List<Block> occupiedBlocks = new List<Block>();
            int width = blockMap.GetLength(0);
            int length = blockMap.GetLength(1);
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < length; ++j)
                {
                    if (blockMap[i, j].Type != BlockType.Empty)
                        occupiedBlocks.Add(blockMap[i, j]);
                }
            }
            foreach (Block occupiedBlock in occupiedBlocks)
            {
                Console.WriteLine("Occupied count: " + occupiedBlocks.Count);
                //Console.WriteLine("in occupied blocks");
                List<Block> empties = new List<Block>();
                foreach (Block b in occupiedBlock.Adjacents)
                {
                    Console.WriteLine(occupiedBlock.Adjacents.Count);
                    if (b.Type == BlockType.Empty)
                    {
                        //Console.WriteLine(b.Type);
                        empties.Add(b);
                    }
                }
                Console.WriteLine("Empties: " + empties.Count);
                if (empties.Count != 0)
                {
                    Console.WriteLine("fuck you");
                    Bogus.Randomizer rand = new Bogus.Randomizer();
                    Creator c = new Creator();
                    Block temp = empties[rand.Number(0, empties.Count - 1)];
                    
                    temp = c.addRoadsToEmptyBlock(temp.StartPoint, this);
                    temp.setBlockType();
                    blockMap[temp.StartPoint.x, temp.StartPoint.z] = temp;
                    c.createBuilding(this, temp);
                    return;
                }

            }
        }
    }
}
