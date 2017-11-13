using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.Canada;
using Bogus;
using ConsoleDump;
using ServerForTheLogic.Utilities;
using ServerForTheLogic.Infrastructure;
using Newtonsoft.Json;
using ServerForTheLogic.Json;
using ServerForTheLogic.Econ;

namespace ServerForTheLogic
{
    /// <summary>
    /// Driver for the City Simulator program
    /// </summary>
    class Program
    {
        public static List<Person> People = new List<Person>();
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;
        private static City city;

        /// <summary>
        /// Entry point for the city simulator program
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
          
            DatabaseLoader loader = new DatabaseLoader();
            city = loader.loadCity();
           // Block b, b1, b2;
            if (city == null)
            {
                //TEST DATA 
                city = new City();
                //fill 3 blocks

                // city.addRoads(city.BlockMap[city.BlockMap.GetLength(1)/2, city.BlockMap.GetLength(0) / 2]);
                city.initialBlockAdd();

                //Commented out Creating a Person
                //Person me;
                //me = creator.createPerson(city);
                //city.AllPeople.Add(me);

            }
            //sets the adjacent blocks of the specified block
            foreach (Block block in city.BlockMap)
            {
                city.setAdjacents(block);
            }

          

            //for (int i = 0; i < city.BlockMap.GetLength(0); i++)
            //{
            //   // city.expandCity();
            //}

            
            foreach (Block block in city.BlockMap)
                if (block.Adjacents.Count > 8)
                    Console.WriteLine("Too many:" + block.ToString());
            //city.printBlockMapTypes();
            printCity();
            Updater<City> updater = new Updater<City>();
            updater.sendFullUpdate(city, Formatting.Indented);
            foo();
            bar();
            KeepOpen();
        }
        /// <summary>
        /// Keeps the program running 
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public static void KeepOpen()
        {
            while (true) ;
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

        /// <summary>
        /// Prints the city represented as symbols/letters in a neatly formatted manner
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public static void printCity()
        {
            for (int i = 0; i < City.CITY_WIDTH; ++i)
            {
                for (int j = 0; j < City.CITY_LENGTH; ++j)
                {
                    if (city.Map[i, j] != null)
                    {
                        Console.Write(city.Map[i, j].Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
        private static void foo()
        {
            Console.WriteLine("FOO");
            Person p = new Person("go", "an", city);
            Console.WriteLine(p.Funds);
            p.BuyThings();
            Console.WriteLine(p.Funds);
        }
        private static void bar()
        {
            Console.WriteLine("BAR");
            Commercial b = new Commercial("fuck", 10);
            Console.WriteLine("funds before "  + b.Funds);
            b.FillInventory();
            Console.WriteLine("funds after " + b.Funds);
        }

    }
}
