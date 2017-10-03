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
        /// Generates an industrial building that has a random company name.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="block"></param>
        public void createIndustrialBuilding(City city, Block block)
        {
            var modelFaker = new Faker<Industrial>("")
                .RuleFor(o => o.Name, f => f.Company.CompanyName());
            Industrial industrial = modelFaker.Generate();
            bool added = false;

            for (int i = 0; i < Block.BLOCK_WIDTH && !added; ++i)
            {
                for (int j = 0; j < Block.BLOCK_LENGTH && !added; ++j)
                {
                    if (block.LandPlot[i, j] == null)
                    {
                        block.LandPlot[i, j] = industrial;
                        city.map[block.StartPoint.x + i, block.StartPoint.z + j] = industrial;
                        added = true;
    }
                }
            }
        }

        /// <summary>
        /// Generates a commercial building with a random company name
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <returns></returns>
        public Commercial createCommercialBuilding()
        {
            var modelFaker = new Faker<Commercial>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName());
            return modelFaker.Generate();
        }

        /// <summary>
        /// Generates a residential building
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <returns></returns>
        public Residential createResidentialBuilding()
        {
            var modelFaker = new Faker<Residential>();
            return modelFaker.Generate();
        }

        /// <summary>
        /// Generate a new block, and fill the border with road.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public Block createBlock(Point startPoint, City city)
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

            return b;
        }
    }
}
