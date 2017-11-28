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

        /// <summary>
        /// Faker object used to generate names for people, businesses etc
        /// </summary>
        private Faker faker;

        /// <summary>
        /// Blocks of Commercial buildings to be filled
        /// </summary>
        [JsonProperty]
        public Queue<Block> CommercialBlocksToFill { get; set; }

        /// <summary>
        /// Blocks of Industrial buildings to be filled
        /// </summary>
        [JsonProperty]
        public Queue<Block> IndustrialBlocksToFill { get; set; }

        /// <summary>
        /// Blocks of Residential buildings to be filled
        /// </summary>
        [JsonProperty]
        public Queue<Block> ResidentialBlocksToFill { get; set; }

        public List<Point> NewRoads { get; set; }

        public List<Building> NewBuildings { get; set; }

        /// <summary>
        /// Max width of the city grid
        /// </summary>
        public const int CITY_WIDTH = 7;//58;

        /// <summary>
        /// Max length of the city grid
        /// </summary>
        public const int CITY_LENGTH = 15;//99;

        [JsonProperty]
        /// <summary>
        /// List of all homes in the city
        /// </summary>
        public List<Residential> Homes { get; set; }

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

        [JsonProperty]
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

        public List<Block> assignedBlocks { get; set; }

        [JsonProperty]
        /// <summary>
        /// Clock to keep track of the time that 
        /// has passed since city creation.
        /// </summary>
        public Clock clock;
        public static int DEFAULT_RATING = 1;

        /// <summary>
        /// Constructor for a new city, creates the grid of cells,
        /// and the grid of city blocks
        /// Initiaiizes all the lists, fills up block map, and sets initial state of city
        /// Written by Connor Goudie, Justin Mclennan, Andrew Busto Chandu Dissanayake
        /// <para/> Last edited:  Chandu Dissanayake, Andrew Busto 2017-11-13
        /// </summary>
        public City()
        {
            Map = new Location[CITY_WIDTH, CITY_LENGTH];
            BlockMap = new Block[CITY_WIDTH / (Block.BLOCK_WIDTH - 1), CITY_LENGTH / (Block.BLOCK_LENGTH - 1)];

            AllPeople = new List<Person>();
            Homes = new List<Residential>();
            //Workplaces = new List<Business>();
            assignedBlocks = new List<Block>();
            faker = new Faker("en");

            NewRoads = new List<Point>();
            NewBuildings = new List<Building>();

            CommercialBlocksToFill = new Queue<Block>();
            IndustrialBlocksToFill = new Queue<Block>();
            ResidentialBlocksToFill = new Queue<Block>();

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
            //sets references to each blocks neighbouring blocks
            foreach(Block b in BlockMap)
            {
                setAdjacents(b);

            }
            //starts clock 
            clock = new Clock(this);

            //sets initial state
            initialBlockAdd();
        }


        /// <summary>
        /// Returns the location object at the specified point
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="p">Point being checked</param>
        /// <returns>Location at point</returns>
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
        /// Initial state of City. 
        /// Currently builds one Industrial block, one Commercial block and one Residential block
        /// </summary>
        /// Written by Andrew Busto, Chandu Dissanayake
        public void initialBlockAdd()
        {
            //Initial block is in the middle of the map
            Block initialBlock = BlockMap[BlockMap.GetLength(0) / 2, BlockMap.GetLength(1) / 2];
            addRoads(initialBlock);
            setAdjacents(initialBlock);
            initialBlock.Type = BlockType.Industrial;
            IndustrialBlocksToFill.Enqueue(initialBlock);
            assignedBlocks.Add(initialBlock);
            createBuilding(initialBlock);

            expandCity(BlockType.Commercial);
            expandCity(BlockType.Residential);

            ClientPacket packet = new ClientPacket(this);
            packet.ConvertPacket();
        }

        /// <summary>
        /// Builds on a "new" block. 
        /// Blocks already exist, but this method allows buildings to be built on the block.
        /// Roads are set and one building is made on the block.
        /// PRE-CONDITION - One block must be initialized manually (done in initialBlockAdd())
        /// </summary>
        /// Written by Andrew Busto, Chandu Dissanayake, Connor Goudie
        /// Last edited by Andrew Busto, Chandu Dissanayake
        /// <param name="type">Type of block being assigned</param>
        /// <returns>the one building on the new block</returns>
        public Building expandCity(BlockType type)
        {
            Console.WriteLine("***CREATING NEW " + type + " BLOCK");

            List<Block> empties = new List<Block>();
            //Console.WriteLine("Occupied count: " + occupiedBlocks.Count);
            foreach (Block assignedBlock in assignedBlocks)
            {
                foreach (Block b in assignedBlock.Adjacents)
                {
                    if (b.Type == BlockType.Empty)
                    {
                        empties.Add(b);
                    }
                }

            }
            
            if (empties.Count != 0)
            {
                Randomizer rand = new Randomizer();
                int randIndex = rand.Number(0, empties.Count - 1);

                addRoads(empties[randIndex]);
                setAdjacents(empties[randIndex]);
                empties[randIndex].Type = type;
                assignedBlocks.Add(empties[randIndex]);
                switch (type)
                {
                    case BlockType.Commercial:
                        CommercialBlocksToFill.Enqueue(empties[randIndex]);
                        break;
                    case BlockType.Residential:
                        ResidentialBlocksToFill.Enqueue(empties[randIndex]);
                        break;
                    case BlockType.Industrial:
                        IndustrialBlocksToFill.Enqueue(empties[randIndex]);
                        break;
                    default:
                        throw new Exception("valid type must be passed in");
                       
                }
                //for (int i = 0; i < 12; i++)
                return createBuilding( BlockMap[empties[randIndex].StartPoint.x / (Block.BLOCK_WIDTH - 1),
                                empties[randIndex].StartPoint.z / (Block.BLOCK_LENGTH - 1)]);
            }
            //if there is no more room to expand.
            else
            {
                Console.WriteLine("no empty blocks");
                return null;
            }
        }

        /// <summary>
        /// Generates a person with an english first and last name using the Faker library
        /// Person is assigned Home, and Workplace. If no home/workplace available, new buildings made.
        /// </summary>
        /// <para>Written by Chandu Dissanayake,Connor Goudie, Justin Mclennan</para>
        /// <returns>Person created</returns>
        public Person createPerson()
        {
            Person temp = new Person(faker.Name.FirstName(), faker.Name.LastName(), this);
            Randomizer rand = new Randomizer();
            List<Residential> randHomes = Homes.OrderBy(x => rand.Int(0, Homes.Count)).ToList();
            //assigns/creates Home
            foreach (Residential r in randHomes)
            {
                if (r.NumberOfResidents < r.Capacity)
                {
                    temp.Home = r;
                    r.NumberOfResidents++;
                    PartialUpdateList[temp.TimeToHome].Add(temp.Id, r.Point);
                    break;
                }
            }
            if (temp.Home == null)
            {
                Residential newHome = (Residential)createBuilding(ResidentialBlocksToFill.Peek());
                temp.Home = newHome;
                newHome.NumberOfResidents++;
                PartialUpdateList[temp.TimeToHome].Add(temp.Id, newHome.Point);
            }

            //assigns/creates jobs
            if(Market.BusinessesHiring.Count == 0)
            {
                createBuilding(CommercialBlocksToFill.Peek());
                createBuilding(IndustrialBlocksToFill.Peek());
            }
            List<Business> fullBusinesses = new List<Business>();
            foreach(Business b in Market.BusinessesHiring)
            {
                if(b.workers.Count < b.Capacity)
                {
                    temp.Workplace = b;
                    b.workers.Add(temp);
                    temp.incomeGenerated(b);
                   // PartialUpdateList[temp.TimeToWork].Add(temp.Id, b.Point);
                    break;
                }
                else
                {
                    fullBusinesses.Add(b);
                }
            }

            foreach(Business b in fullBusinesses)
            {
                Market.BusinessesHiring.Remove(b);
            }
            AllPeople.Add(temp);

            return temp;
        }

        /// <summary>
        /// Generates a building on the block being passed in.
        /// If block is determined to be full, calls handleFullBlock method
        /// </summary>
        /// <para>Written by Chandu Dissanayake,Andrew Busto, Connor Goudie 2017-10-27 </para>
        /// <returns></returns>
        public Building createBuilding(Block block)
        {
            Console.WriteLine("Creating new " + block.Type + "building");
            Building building;
            List<Point> availablePoints = new List<Point>();
            for (int i = 0; i < Block.BLOCK_WIDTH; ++i)
            {
                for (int j = 0; j < Block.BLOCK_LENGTH; ++j)
                {
                    if (block.LandPlot[i, j] == null)
                    {
                        availablePoints.Add(new Point(i, j));
                    }
                }
            }

            if (availablePoints.Count == 0)
            {
                return handleFullBlock(block);
            }

            int rand = new Randomizer().Number(0, availablePoints.Count - 1);
            int x = availablePoints[rand].x;
            int z = availablePoints[rand].z;

            if (block.Type == BlockType.Commercial)
            {
                building = new Commercial(faker.Company.CompanyName(), FIXED_CAPACITY,true);
            }
            else if (block.Type == BlockType.Residential)
            {
                building = new Residential(FIXED_CAPACITY,true);
                Homes.Add((Residential)building);
                               
            }
            else if (block.Type == BlockType.Industrial)
            {
                building = new Industrial(faker.Company.CompanyName(), FIXED_CAPACITY,true);
            }
            else
            {
                throw new InvalidOperationException("cannot add building to empty block");
            }
            building.Point = new Point(block.StartPoint.x + x, block.StartPoint.z + z);
            block.LandPlot[x, z] = building;
            Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
            NewBuildings.Add(new Building(building));
            return building;
        }

        /// <summary>
        /// Handles situation when creating building and block is full.
        /// Will pop value from queue, 
        /// if queue is empty, it will expandCity(), else it will create a building with the next block on the queue
        /// </summary>
        /// <para>Written by Chandu Dissanayake 2017-11-14 </para>
        /// <para>Last modified by Chandu Dissanayake 2017-11-14 </para>
        /// <param name="block">Block that is full</param>
        /// <returns>Building that is newly made, on the next block in the corresponding queue</returns>
        private Building handleFullBlock(Block block)
        {
            switch (block.Type)
            {
                case BlockType.Commercial:
                    CommercialBlocksToFill.Dequeue();
                    if (CommercialBlocksToFill.Count == 0)
                    {
                        return expandCity(BlockType.Commercial);
                    }
                    else
                    {
                        return createBuilding(CommercialBlocksToFill.Peek());
                    }
                case BlockType.Industrial:
                    IndustrialBlocksToFill.Dequeue();
                    if (IndustrialBlocksToFill.Count == 0)
                    {
                        return expandCity(BlockType.Industrial);
                    }
                    else
                    {
                        return createBuilding(IndustrialBlocksToFill.Peek());
                    }
                case BlockType.Residential:
                    ResidentialBlocksToFill.Dequeue();
                    if (ResidentialBlocksToFill.Count == 0)
                    {
                        return expandCity(BlockType.Residential);
                    }
                    else
                    {
                        return createBuilding(ResidentialBlocksToFill.Peek());
                    }
                default:
                    Console.WriteLine("handleFullBlock - TYPE NOT IDENTIFIED");
                    return null;

            }
        }

        /// <summary>
        /// Fill the border of a block with road.
        /// </summary>
        /// <param name="b">Block that is getting roads</param>
        /// <returns></returns>
        /// Written by : Connor Goudie
        /// Updated on: 27-11-2017
        /// Updated by: Chandu Dissanayake
        /// Changes: Adds new roads to List that will be serialized into Json for client
        public void addRoads(Block b)
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
                    NewRoads.Add(new Point(i + xPos, zPos));
                }

                if (GetLocationAt(i + xPos, zPos + length) != null)
                {
                    b.LandPlot[i, length] = GetLocationAt(i + xPos, zPos + length);
                }
                else
                {
                    b.LandPlot[i, length] = new Road("");
                    Map[i + xPos, zPos + length] = b.LandPlot[i, length];
                    NewRoads.Add(new Point(i + xPos, zPos + length));

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
                    NewRoads.Add(new Point(xPos, i + zPos));

                }
                if (GetLocationAt(xPos + width, i + zPos) != null)
                {
                    b.LandPlot[width, i] = GetLocationAt(xPos + width, i + zPos);
                }
                else
                {
                    b.LandPlot[width, i] = new Road("");
                    Map[xPos + width, i + zPos] = b.LandPlot[width, i];
                    NewRoads.Add(new Point(xPos + width, i + zPos));

                }
            }
            // b.setBlockType();

            //city.BlockMap[xPos / width, zPos / length] = b;
            // return b;
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
                    //Console.WriteLine("continuing at X: " + x + " I+X: " + i);
                    continue;
                }
                for (int j = z - 1; j < z + 2; ++j)
                {
                    //if out of bounds of the map, or on the current block's cell, skip
                    if (j < 0 || j >= BlockMap.GetLength(1) || (j == z && i == x))
                    {
                        //Console.WriteLine("continuing at X: " + x + " Z: " + z + " I+X: " + i + " J+Z: " + j);
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
                        Console.WriteLine("invalid state in setadjacents");
                        BlockMap[x, z].Adjacents.Add(null);
                    }
                }

            }
        }

        /// <summary>
        /// Prints the city represented as symbols/letters in a neatly formatted manner
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public void printCity()
        {
            for (int i = 0; i < City.CITY_WIDTH; ++i)
            {
                for (int j = 0; j < City.CITY_LENGTH; ++j)
                {
                    if (Map[i, j] != null)
                    {
                        Console.Write(Map[i, j].Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Prints a block represented as symbols/letters in a neatly formatted manner
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="b"></param>
        public static void printBlock(Block b)
        {
            for (int i = 0; i < Block.BLOCK_WIDTH; i++)
            {
                for (int j = 0; j < Block.BLOCK_LENGTH; j++)
                {
                    if (b.LandPlot[i, j] != null)
                    {
                        Console.Write(b.LandPlot[i, j].Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

    }

}
