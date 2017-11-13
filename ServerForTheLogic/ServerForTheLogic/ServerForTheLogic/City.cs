using Newtonsoft.Json;
using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Json;
using ServerForTheLogic.Utilities;
using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace ServerForTheLogic
{
    [JsonObject(MemberSerialization.OptIn)]
    class City
    {

        public static int FIXED_CAPACITY = 50;

        private Faker faker;

        [JsonProperty]
        /// <summary>
        /// Max width of the city grid
        /// </summary>
        public const int CITY_WIDTH = 58;
        [JsonProperty]
        /// <summary>
        /// Max length of the city grid
        /// </summary>
        public const int CITY_LENGTH = 99;

        [JsonProperty]
        /// <summary>
        /// List of all homes in the city
        /// </summary>
        public List<Residential> Homes { get; set; }
        [JsonProperty]
        /// <summary>
        /// List of all places of work in the city
        /// </summary>
        public List<Business> Workplaces { get; set; }
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
        public static int DEFAULT_RATING = 1;

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
            Workplaces = new List<Business>();

            faker = new Faker("en");


            PartialUpdateList = new Dictionary<int, Dictionary<Guid, Point>>();
            for (int i = 0; i < 24; ++i)
                PartialUpdateList.Add(i, new Dictionary<Guid, Point>());

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
            clock = new Clock(this);

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
                foreach (Block b in occupiedBlock.Adjacents)
                {
                    if (b.Type == BlockType.Empty)
                    {
                        //Console.WriteLine(b.Type);
                        empties.Add(b);
                    }
                }

            }
            
            if (empties.Count != 0)
            {
                Randomizer rand = new Randomizer();
                int index = rand.Number(0, empties.Count - 1);

                //empties[index] = c.addRoadsToEmptyBlock(empties[index], this);
                addRoadsToEmptyBlock(empties[index]);
                setAdjacents(empties[index]);

                //for (int i = 0; i < 12; i++)
                createBuilding( BlockMap[empties[index].StartPoint.x / (Block.BLOCK_WIDTH - 1),
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

        /// <summary>
        /// Generates a person with an english first and last name.
        /// </summary>
        /// <returns></returns>
        public Person createPerson()
        {
            Person temp = new Person(faker.Name.FirstName(), faker.Name.LastName(), this);
            Randomizer rand = new Randomizer();
            List<Residential> randHomes = Homes.OrderBy(x => rand.Int(0,Homes.Count)).ToList();
            foreach (Residential r in randHomes)
            {
                if (r.NumberOfResidents < r.Capacity)
                {
                    temp.Home = r;
                    r.NumberOfResidents++;
                    PartialUpdateList[temp.TimeToGoToHome].Add(temp.Id, r.Point);
                    break;
                }
            }
            if (temp.Home == null)
            {
                //MAKE NEW RESIDENTIAL BUILDING
            }


            //Had an error here
            //Business business = Market.BusinessesHiring[new Random().Next(Market.BusinessesHiring.Count)];
            //temp.Workplace = business;
            //city.PartialUpdateList[temp.TimeToGoToWork].Add(temp.Id, business.Point);

            return temp;
        }

        /// <summary>
        /// Generates a building based on the block's BlockType
        /// </summary>
        /// <returns></returns>
        public void createBuilding(Block block)
        {
            List<Point> availablePoints = new List<Point>();
            for (int i = 0; i < Block.BLOCK_WIDTH; ++i)
            {
                for (int j = 0; j < Block.BLOCK_LENGTH; ++j)
                {
                    if (block.LandPlot[i, j] == null)
                    {
                        //block.LandPlot[i, j] = industrial;
                        //city.map[block.StartPoint.x + i, block.StartPoint.z + j] = industrial;
                        //added = true;
                        availablePoints.Add(new Point(i, j));
                    }
                }
            }

            int rand = new Randomizer().Number(0, availablePoints.Count - 1);
            int x = availablePoints[rand].x;
            int z = availablePoints[rand].z;

            if (block.Type == BlockType.Commercial)
            {
                Commercial building = new Commercial(faker.Company.CompanyName(), FIXED_CAPACITY);
                Market.CommercialBusinesses.Add(building);
                Market.BusinessesHiring.Add(building);
                block.LandPlot[x, z] = building;
                Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;

            }
            else if (block.Type == BlockType.Residential)
            {
                Residential building = new Residential(FIXED_CAPACITY);
                Homes.Add(building);
                block.LandPlot[x, z] = building;
                Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
                if (building.IsTall)
                    building.NumberOfResidents = Residential.CAPACITY_TALL;
            }
            else if (block.Type == BlockType.Industrial)
            {
                Industrial building = new Industrial(faker.Company.CompanyName(), FIXED_CAPACITY);
                Market.IndustrialBusinesses.Add(building);
                Market.BusinessesHiring.Add(building);
                block.LandPlot[x, z] = building;
                Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
                // city.Workplaces.Add(building);
            }
            else
            {
                throw new InvalidOperationException("cannot add building to empty block");
            }


        }

        /// <summary>
        /// Fill the border of a block with road.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        /// Updated on: 18-10-2017
        /// Updated by: Connor Goudie
        /// Changes: Refactored for readability (no functionality changes)
        public void addRoadsToEmptyBlock(Block b)
        {
            int xPos = b.StartPoint.x;
            int zPos = b.StartPoint.z;
            int width = Block.BLOCK_WIDTH - 1;
            int length = Block.BLOCK_LENGTH - 1;
            // Adds roads to the top and bottom borders of the block grid
            for (int i = 0; i < Block.BLOCK_WIDTH; i++)
            {
                if (GetLocationAt(i + xPos, zPos) != null)
                {
                    b.LandPlot[i, 0] = GetLocationAt(i + xPos, zPos);
                }
                else
                {
                    b.LandPlot[i, 0] = new Road("");
                    Map[i + xPos, zPos] = b.LandPlot[i, 0];
                }

                if (GetLocationAt(i + xPos, zPos + length) != null)
                {
                    b.LandPlot[i, length] = GetLocationAt(i + xPos, zPos + length);
                }
                else
                {
                    b.LandPlot[i, length] = new Road("");
                    Map[i + xPos, zPos + length] = b.LandPlot[i, length];
                }
            }

            //adds roads to the left and right borders of the block grid
            for (int i = 0; i < Block.BLOCK_LENGTH; i++)
            {
                if (GetLocationAt(xPos, i + zPos) != null)
                {
                    b.LandPlot[0, i] = GetLocationAt(xPos, i + zPos);
                }
                else
                {
                    b.LandPlot[0, i] = new Road("");
                    Map[xPos, i + zPos] = b.LandPlot[0, i];
                }

                if (GetLocationAt(xPos + width, i + zPos) != null)
                {
                    b.LandPlot[width, i] = GetLocationAt(xPos + width, i + zPos);
                }
                else
                {
                    b.LandPlot[width, i] = new Road("");
                    Map[xPos + width, i + zPos] = b.LandPlot[width, i];
                }
            }
            b.setBlockType();

            //city.BlockMap[xPos / width, zPos / length] = b;
            // return b;
        }

    }


}
