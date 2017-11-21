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

               
                city.initialBlockAdd();

             
            
            }
          
            //city.printBlockMapTypes();
            city.printCity();
            Updater<City> updater = new Updater<City>();
            updater.sendFullUpdate(city, Formatting.Indented);
            //foo();
            bar();
            while (true) {
                Console.WriteLine("Enter Command:");
                String cmd = Console.ReadLine();
                //Console.WriteLine(cmd);
                String []commands = cmd.Split(null);

                if (commands[0].Equals("people", StringComparison.CurrentCultureIgnoreCase)) {
                    if (commands.Length == 2) {
                        int number = Int32.Parse(commands[1]);
                        //Console.WriteLine(number);
                        for (int i = 0; i < number; i++) {
                            city.createPerson();

                        }
                    }
                    city.AllPeople.Dump();
                }
                if(commands[0].Equals("workplaces", StringComparison.CurrentCultureIgnoreCase)) {
                    city.Workplaces.Dump();
                }

                if (commands[0].Equals("homes", StringComparison.CurrentCultureIgnoreCase)) {
                    city.Homes.Dump();
                }
                if (commands[0].Equals("expand", StringComparison.CurrentCultureIgnoreCase)) {
                    if(commands.Length == 2) {
                        if (commands[1].Equals("residential", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Residential);
                        if (commands[1].Equals("commercial", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Commercial);
                        if (commands[1].Equals("industrial", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Industrial);
                    } else {
                        Console.WriteLine("Specify expansion type");
                    }
                }

                if (cmd.Equals("print city", StringComparison.CurrentCultureIgnoreCase)) {
                    city.printCity();
                }
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

        
        private static void foo()
        {
            //Console.WriteLine("FOO");
            Person p = city.createPerson();
            Console.WriteLine("funds before " + p.Funds);
            p.BuyThings();
            Console.WriteLine("funds after " + p.Funds);
        }
        private static void bar()
        {
            //Console.WriteLine("BAR");
            Commercial b = new Commercial("fuck", 10,true);
            Console.WriteLine("funds before "  + b.Funds);
            b.FillInventory();
            //Console.WriteLine("funds after " + b.Funds);
        }

    }
}
