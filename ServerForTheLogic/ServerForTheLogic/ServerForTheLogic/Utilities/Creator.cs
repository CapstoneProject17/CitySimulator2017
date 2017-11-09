using Bogus;
using ServerForTheLogic.Econ;
using ServerForTheLogic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace ServerForTheLogic.Utilities
{

    /// <summary>
    /// Creator holds all of the methods to instantiate other objects
    /// </summary>
    class Creator
    {
        public static int FIXED_CAPACITY = 50;

        private Faker faker;

        public Creator()
        {
            faker = new Faker("en");
        }

        /// <summary>
        /// Generates a person with an english first and last name.
        /// </summary>
        /// <returns></returns>
        public Person createPerson(City city)
        {
            Person temp = new Person(faker.Name.FirstName(), faker.Name.LastName(), city);
            Randomizer rand = new Randomizer(); 
            List<Residential> randHomes = city.Homes.OrderBy(x => rand.Int()).ToList();
            foreach (Residential r in randHomes)
            {
                if (r.NumberOfResidents < r.Capacity)
                {
                    temp.Home = r;
                    r.NumberOfResidents++;
                    city.PartialUpdateList[temp.TimeToGoToHome].Add(temp.Id, r.Point);
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
        public void createBuilding(City city, Block block)
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
                city.Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;

            }
            else if (block.Type == BlockType.Residential)
            {
                Residential building = new Residential(FIXED_CAPACITY);
                city.Homes.Add(building);
                block.LandPlot[x, z] = building;
                city.Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
                if (building.IsTall)
                    building.NumberOfResidents = Residential.CAPACITY_TALL;
            }
            else if (block.Type == BlockType.Industrial)
            {
                Industrial building = new Industrial(faker.Company.CompanyName(), FIXED_CAPACITY);
                Market.IndustrialBusinesses.Add(building);
                Market.BusinessesHiring.Add(building);
                block.LandPlot[x, z] = building;
                city.Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
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
        public void addRoadsToEmptyBlock(Block b, City city)
        {
            int xPos = b.StartPoint.x;
            int zPos = b.StartPoint.z;
            int width = Block.BLOCK_WIDTH - 1;
            int length = Block.BLOCK_LENGTH - 1;
            // Adds roads to the top and bottom borders of the block grid
            for (int i = 0; i < Block.BLOCK_WIDTH; i++)
            {
                if (city.GetLocationAt(i + xPos, zPos) != null)
                {
                    b.LandPlot[i, 0] = city.GetLocationAt(i + xPos, zPos);
                }
                else
                {
                    b.LandPlot[i, 0] = new Road("");
                    city.Map[i + xPos, zPos] = b.LandPlot[i, 0];
                }

                if (city.GetLocationAt(i + xPos, zPos + length) != null)
                {
                    b.LandPlot[i, length] = city.GetLocationAt(i + xPos, zPos + length);
                }
                else
                {
                    b.LandPlot[i, length] = new Road("");
                    city.Map[i + xPos, zPos + length] = b.LandPlot[i, length];
                }
            }

            //adds roads to the left and right borders of the block grid
            for (int i = 0; i < Block.BLOCK_LENGTH; i++)
            {
                if (city.GetLocationAt(xPos, i + zPos) != null)
                {
                    b.LandPlot[0, i] = city.GetLocationAt(xPos, i + zPos);
                }
                else
                {
                    b.LandPlot[0, i] = new Road("");
                    city.Map[xPos, i + zPos] = b.LandPlot[0, i];
                }

                if (city.GetLocationAt(xPos + width, i + zPos) != null)
                {
                    b.LandPlot[width, i] = city.GetLocationAt(xPos + width, i + zPos);
                }
                else
                {
                    b.LandPlot[width, i] = new Road("");
                    city.Map[xPos + width, i + zPos] = b.LandPlot[width, i];
                }
            }
            b.setBlockType();
            //city.BlockMap[xPos / width, zPos / length] = b;
            // return b;
        }
    }
}
