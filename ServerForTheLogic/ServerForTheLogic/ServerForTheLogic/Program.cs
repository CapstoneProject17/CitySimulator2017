using System;
using System.Collections.Generic;
using ConsoleDump;
using ServerForTheLogic.Infrastructure;
using Newtonsoft.Json;
using ServerForTheLogic.Json;
using ServerForTheLogic.Econ;
using System.ServiceProcess;
using NLog;

namespace ServerForTheLogic
{
    /// <summary>
    /// Driver for the City Simulator program
    /// </summary>
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public const string ServiceName = "ServerForTheLogic";
        public static List<Person> People = new List<Person>();
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;
        private static City city;


        public static void Start(string[] args)
        {
            DatabaseLoader loader = new DatabaseLoader();
            city = loader.loadCity();
            // Block b, b1, b2;
            if (city == null)
            {
                //TEST DATA 
                city = new City();
                //fill 3 blocks

            }

            //city.printBlockMapTypes();
            city.printCity();
            Updater<City> updater = new Updater<City>();
            updater.sendFullUpdate(city, Formatting.Indented);
            //foo();
            test2();
            GetInput();
        }

        public static void Stop()
        {
            // onstop code here
        }

        /// <summary>
        /// Entry point for the city simulator program
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            logger.Info("its a bug");
            logger.Info("its a bug");
            logger.Info("its a bug");
            logger.Info("its a bug");
            if (!Environment.UserInteractive)
            {
                Console.WriteLine("service");
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            }
            else
            {
                // running as console app
                Console.WriteLine("console");
                DatabaseLoader loader = new DatabaseLoader();
                city = loader.loadCity();
                // Block b, b1, b2;
                if (city == null)
                {
                    //TEST DATA 
                    city = new City();
                    //fill 3 blocks

                }

                //city.printBlockMapTypes();
                city.printCity();
                Updater<City> updater = new Updater<City>();
                updater.sendFullUpdate(city, Formatting.Indented);
                //foo();
                test2();
                GetInput();
            }
        }


        /// <summary>
        /// Test method for Market transaction (Person)
        /// </summary>
        private static void test1()
        {
            Console.WriteLine("FOO");
            Person p = city.createPerson();
            Console.WriteLine("funds before " + p.Funds);
            p.BuyThings();
            Console.WriteLine("funds after " + p.Funds);
        }
        /// <summary>
        /// Test method for Market transaction (Commercial)
        /// </summary>
        private static void test2()
        {
            Console.WriteLine("BAR");
            Commercial b = new Commercial("new", 10, true);
            Console.WriteLine("funds before " + b.Funds);
            b.FillInventory();
            //Console.WriteLine("funds after " + b.Funds);
        }

        private static void GetInput()
        {
            while (true)
            {
                Console.WriteLine("Enter Command:");
                String cmd = Console.ReadLine();
                //Console.WriteLine(cmd);
                String[] commands = cmd.Split(null);

                if (commands[0].Equals("people", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (commands.Length == 2)
                    {
                        int number = Int32.Parse(commands[1]);
                        //Console.WriteLine(number);
                        for (int i = 0; i < number; i++)
                        {
                            city.createPerson();

                        }
                    }
                    city.AllPeople.Dump();
                }
                if (commands[0].Equals("workplaces", StringComparison.CurrentCultureIgnoreCase))
                {
                    //city.Workplaces.Dump();
                }
                if (commands[0].Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.clock.timer.Stop();
                }
                if (commands[0].Equals("start", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.clock.timer.Start();
                }
                if (commands[0].Equals("homes", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.Homes.Dump();
                }
                if (commands[0].Equals("expand", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (commands.Length == 2)
                    {
                        if (commands[1].Equals("residential", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Residential);
                        if (commands[1].Equals("commercial", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Commercial);
                        if (commands[1].Equals("industrial", StringComparison.CurrentCultureIgnoreCase))
                            city.expandCity(BlockType.Industrial);
                    }
                    else
                    {
                        Console.WriteLine("Specify expansion type");
                    }
                }

            }
        }
    }
}

