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
            }
            else if (block.Type == BlockType.Residential)
            {
                var modelFaker = new Faker<Residential>();
                building = modelFaker.Generate();

            }
            else
            {
                var modelFaker = new Faker<Industrial>("")
                    .RuleFor(o => o.Name, f => f.Company.CompanyName());
                building = modelFaker.Generate();
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

            int rand = new Random().Next(0, availablePoints.Count);
            int x = availablePoints[rand].x;
            int z = availablePoints[rand].z;
            block.LandPlot[x, z] = building;
            city.map[block.StartPoint.x + x, block.StartPoint.z + z] = building;
        }

        /// <summary>
        /// Fill the border of a block with road.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public Block addRoadsToEmptyBlock(Point startPoint, City city)
        {
            Block b = new Block(startPoint);

            // Adds roads to the top and bottom borders of the block grid
            for (int i = 0; i < Block.BLOCK_WIDTH; i++)
            {
                if (city.GetLocationAt(i + startPoint.x, startPoint.z) != null)
                {
                    b.LandPlot[i, 0] = city.GetLocationAt(i + startPoint.x, startPoint.z);
                }
                else
                {
                    b.LandPlot[i, 0] = new Road("");
                    city.map[i + startPoint.x, startPoint.z] = b.LandPlot[i, 0];
                }

                if (city.GetLocationAt(i + startPoint.x, startPoint.z + Block.BLOCK_LENGTH - 1) != null)
                {
                    b.LandPlot[i, Block.BLOCK_LENGTH - 1] = city.GetLocationAt(i + startPoint.x, startPoint.z + Block.BLOCK_LENGTH - 1);
                }
                else
                {
                    b.LandPlot[i, Block.BLOCK_LENGTH - 1] = new Road("");
                    city.map[i + startPoint.x, startPoint.z + Block.BLOCK_LENGTH - 1] = b.LandPlot[i, Block.BLOCK_LENGTH - 1];
                }
            }

            //adds roads to the left and right borders of the block grid
            for (int i = 0; i < Block.BLOCK_LENGTH; i++)
            {
                if (city.GetLocationAt(startPoint.x, i + startPoint.z) != null)
                {
                    b.LandPlot[0, i] = city.GetLocationAt(startPoint.x, i + startPoint.z);
                }
                else
                {
                    b.LandPlot[0, i] = new Road("");
                    city.map[startPoint.x, i + startPoint.z] = b.LandPlot[0, i];
                }

                if (city.GetLocationAt(startPoint.x + Block.BLOCK_WIDTH - 1, i + startPoint.z) != null)
                {
                    b.LandPlot[Block.BLOCK_WIDTH - 1, i] = city.GetLocationAt(startPoint.x + Block.BLOCK_WIDTH - 1, i + startPoint.z);
                }
                else
                {
                    b.LandPlot[Block.BLOCK_WIDTH - 1, i] = new Road("");
                    city.map[startPoint.x + Block.BLOCK_WIDTH - 1, i + startPoint.z] = b.LandPlot[Block.BLOCK_WIDTH - 1, i];
                }
            }
            city.blockMap[b.StartPoint.x / (Block.BLOCK_WIDTH - 1), b.StartPoint.z / (Block.BLOCK_LENGTH - 1)] = b;
            return b;
        }
    }
}
