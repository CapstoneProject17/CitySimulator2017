using Newtonsoft.Json;
using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Json;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic
{
    [JsonObject(MemberSerialization.OptIn)]
    class City
    {
        [JsonProperty]
        /// <summary>
        /// Max width of the city grid
        /// </summary>
        public const int CITY_WIDTH = 100;
        [JsonProperty]
        /// <summary>
        /// Max length of the city grid
        /// </summary>
        public const int CITY_LENGTH = 204;

        [JsonProperty]
        /// <summary>
        /// List of all homes in the city
        /// </summary>
        public List<Residential> Homes { get; set; }
        [JsonProperty]
        /// <summary>
        /// List of all places of work in the city
        /// </summary>
        public List<Building> Workplaces { get; set; }
        [JsonProperty]
        /// <summary>
        /// List of all inhabitants of the city
        /// </summary>
        public List<Person> AllPeople { get; set; }
        /// <summary>
        /// Every hour, send the nested dictionary to the network queue
        /// </summary>
        public Dictionary<int, Dictionary<Guid, Point>> PartialUpdateList { get; private set; }

        /// <summary>
        /// Dictionary of Locations to send to client when they connect
        /// </summary>
        public Dictionary<Guid, Pair<Point, BlockType>> OnLoadLocations { get; set; }

        /// <summary>
        /// Dictionary of all people to send to clients when they connect
        /// </summary>
        public Dictionary<Guid, Point> OnLoadPeople { get; set; }
        /// <summary>
        /// The grid that all buildings/roads/people exist in
        /// </summary>
        public Location[,] Map { get; set; }
        [JsonProperty]
        /// <summary>
        /// 2D array of city blocks, this is for easier city expansion
        /// NOTE: not currently implemented
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public Block[,] BlockMap { get; set; }

        [JsonProperty]
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
            Map = new Location[CITY_WIDTH, CITY_LENGTH];
            BlockMap = new Block[CITY_WIDTH / (Block.BLOCK_WIDTH - 1),
                                 CITY_LENGTH / (Block.BLOCK_LENGTH - 1)];
            AllPeople = new List<Person>();
            Homes = new List<Residential>();
            Workplaces = new List<Building>();
            PartialUpdateList = new Dictionary<int, Dictionary<Guid, Point>>();

            int width = CITY_WIDTH / (Block.BLOCK_WIDTH - 1);
            int height = CITY_LENGTH / (Block.BLOCK_LENGTH - 1);
            //fills the blockMap array
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    BlockMap[i, j] = new Block(
                        new Point(i * (Block.BLOCK_WIDTH - 1),
                        j * (Block.BLOCK_LENGTH - 1))
                    );
                }
            }
            // clock = new Clock(this);

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
            return Map[p.x, p.z];
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
            return Map[x, z];
        }

        /// <summary>
        /// Adds references to a specified block's adjacent blocks.
        /// </summary>
        /// <param name="b"></param>
        public void setAdjacents(Block b)
        {
            if (b.Adjacents.Count > 0)
                return;
            int x = b.StartPoint.x / (Block.BLOCK_WIDTH - 1);
            int z = b.StartPoint.z / (Block.BLOCK_LENGTH - 1);
            //Console.WriteLine(blockMap.GetLength(0) + " " + blockMap.GetLength(1));
            for (int i = x - 1; i < x + 2; ++i)
            {
                //if out of bounds of the map, skip
                if (i < 0 || i >= BlockMap.GetLength(0))
                {
                    //Console.WriteLine("WE OUT X: " + x + " I+X: " + i);
                    continue;
                }
                for (int j = z - 1; j < z + 2; ++j)
                {
                    //if out of bounds of the map, or on the current block's cell, skip
                    if (j < 0 || j >= BlockMap.GetLength(1) || (j == z && i == x))
                    {
                        //Console.WriteLine("WE OUT X: " + x + " Z: " + z + " I+X: " + i + " J+Z: " + j);
                        continue;
                    }
                    //checks if adjacent block is null (though it should never be null)
                    if (BlockMap[i, j] != null)
                    {
                        //Console.WriteLine("X: " + x + " Z: " + z + " I: " + i + " J: " + j);
                        BlockMap[x, z].Adjacents.Add(BlockMap[i, j]);
                    }
                    else
                    {
                        //Console.WriteLine("how?");
                        BlockMap[x, z].Adjacents.Add(null);
                    }
                }

            }
        }

        public void expandCity()
        {
            List<Block> occupiedBlocks = new List<Block>();

            foreach (Block b in BlockMap)
                if (b.Type != BlockType.Empty)
                    occupiedBlocks.Add(b);

            List<Block> empties = new List<Block>();
            //Console.WriteLine("Occupied count: " + occupiedBlocks.Count);
            foreach (Block occupiedBlock in occupiedBlocks)
            {
                //Console.WriteLine("in occupied blocks");
                //Console.WriteLine("Adjacent Count: " + occupiedBlock.Adjacents.Count);
                foreach (Block b in occupiedBlock.Adjacents)
                {
                    if (b.Type == BlockType.Empty)
                    {
                        //Console.WriteLine(b.Type);
                        empties.Add(b);
                    }
                }

            }
            //foreach (Block b in empties)
            //    Console.WriteLine(b.ToString());
            //Console.WriteLine("Empties: " + empties.Count);
            if (empties.Count != 0)
            {
                Bogus.Randomizer rand = new Bogus.Randomizer();
                Creator c = new Creator();

                int index = rand.Number(0, empties.Count - 1);

                empties[index] = c.addRoadsToEmptyBlock(empties[index], this);
                setAdjacents(empties[index]);
                for (int i = 0; i < 12; i++)
                    c.createBuilding(this,
                        BlockMap[empties[index].StartPoint.x / (Block.BLOCK_WIDTH - 1),
                                 empties[index].StartPoint.z / (Block.BLOCK_LENGTH - 1)]);
            }
        }

        public void printBlockMapTypes()
        {
            foreach (Block b in BlockMap)
            {
                Console.WriteLine(b.Type);
            }
        }
    }
}
