using Bogus;
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
        /// <summary>
        /// Generates a person with an english first and last name.
        /// </summary>
        /// <returns></returns>
        public Person createPerson()
        {
            var modelFaker = new Faker<Person>("en")
                .RuleFor(o => o.FName, (f, o) => f.Name.FirstName())
                .RuleFor(o => o.LName, (f, o) => f.Name.LastName());

            return modelFaker.Generate();
        }

        /// <summary>
        /// Generates a building based on the block's BlockType
        /// </summary>
        /// <returns></returns>
        public void createBuilding(City city, Block block)
        {
            Building building;
            if (block.Type == BlockType.Commercial)
            {
                var modelFaker = new Faker<Commercial>()
                    .RuleFor(o => o.Name, f => f.Company.CompanyName());
                building = modelFaker.Generate();
                city.Workplaces.Add(building);
            }
            else if (block.Type == BlockType.Residential)
            {
                var modelFaker = new Faker<Residential>();
                building = modelFaker.Generate();
                city.Homes.Add((Residential)building);

            }
            else if (block.Type == BlockType.Industrial)
            {
                var modelFaker = new Faker<Industrial>("")
                    .RuleFor(o => o.Name, f => f.Company.CompanyName());
                building = modelFaker.Generate();
                city.Workplaces.Add(building);
            }
            else
            {
                throw new InvalidOperationException("cannot add building to empty block");
            }
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
            block.LandPlot[x, z] = building;
            city.Map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
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
        public Block addRoadsToEmptyBlock(Block b, City city)
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
            city.BlockMap[xPos / width, zPos / length] = b;
            return b;
        }
    }
}
