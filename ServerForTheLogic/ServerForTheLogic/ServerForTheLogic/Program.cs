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
        private static Creator creator;

        /// <summary>
        /// Entry point for the city simulator program
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ////Person me = new Person();
            ////me.Dump();
            //Person me = new Person();
            //for (int i = 0; i < 24; i++) {
            //    me = new Person();
            //   // me.createPerson();
            //    People.Add(me);
            //    me = null;
            //}
            //People.Dump();
            //me = new Person();
            //// me.KeepOpen();
            //KeepOpen();

            //TEST DATA 
            city = new City();
            creator = new Creator();
            Block b = creator.createBlock(new Point(City.CITY_WIDTH / 2, City.CITY_LENGTH / 2), city);
            Block b1 = creator.createBlock(new Point(City.CITY_WIDTH / 2 + 3, City.CITY_LENGTH / 2), city);
            Block b2 = creator.createBlock(new Point(0, 0), city);
            creator.createIndustrialBuilding(city, b);
            creator.createIndustrialBuilding(city, b1);
            creator.createIndustrialBuilding(city, b2);

            printCity();


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
                        Console.Write("o");
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
            for (int i = 0; i < City.CITY_LENGTH; ++i)
            {
                for (int j = 0; j < City.CITY_WIDTH; ++j)
                {
                    if (city.map[i, j] != null)
                    {
                        Console.Write(city.map[i, j].Type);
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
