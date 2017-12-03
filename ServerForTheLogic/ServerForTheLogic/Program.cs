using System;
using System.Collections.Generic;
using ConsoleDump;
using Newtonsoft.Json;
using ServerForTheLogic.Json;
using NLog;
using System.IO;
using CitySimNetworkService;
using DBInterface;
using DBInterface.Infrastructure;
using DataAccessLayer;
using DBInterface.Econ;

namespace ServerForTheLogic
{
    /// <summary>
    /// Driver for the City Simulator program
    /// </summary>
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static SimulationStateQueue fullUpdateQueue = new SimulationStateQueue
        {
            StateBufferSize = 1
        };

        private static SimulationStateQueue partialUpdateQueue = new SimulationStateQueue
        {
            StateBufferSize = 25
        };

        public const string ServiceName = "ServerForTheLogic";
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
            string path = @"..\..\SerializedCity\city.json";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }

            // Open the file to read from.
            string readText = File.ReadAllText(path);
            //Console.WriteLine(readText);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new LocationConverter());
            settings.Converters.Add(new BlockConverter());
            //JsonSerializer serializer = new JsonSerializer();
            city = JsonConvert.DeserializeObject<City>(readText, settings);



            if (city == null)
            {
                city = new City(fullUpdateQueue, partialUpdateQueue);
            }

            //city.CommercialBlocksToFill.Dump();
            //city.PartialUpdateList.Dump();
            city.printCity();
            

            foreach (Block b in city.BlockMap)
                if (b.Type != BlockType.Empty)
                {
                    city.addRoads(b);
                }
                    
            int max = 0;
            foreach (Person p in city.AllPeople)
            {
                if (p.DaysLeft > max)
                    max = p.DaysLeft;
            }
            Console.WriteLine(max);
            city.InitSimulation(fullUpdateQueue, partialUpdateQueue);
            foreach (Block b in city.BlockMap)
                city.setAdjacents(b);


            
            GetInput();
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
                    if (commands.Length == 1)
                    {
                        city.AllPeople.Dump();
                    }
                    if (commands.Length == 2)
                    {
                        try
                        {
                            int number = Int32.Parse(commands[1]);
                            //Console.WriteLine(number);
                            for (int i = 0; i < number; i++)
                            {
                                city.createPerson();
                            }
                            Console.WriteLine("Added" + number + " people");
                        }
                        catch { }
                    }

                }
                if (commands[0].Equals("workplaces", StringComparison.CurrentCultureIgnoreCase))
                {
                    Market.CommercialBusinesses.Dump();
                    Market.IndustrialBusinesses.Dump();
                }
                if (commands[0].Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.StopSimulation();
                }
                if (commands[0].Equals("start", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.clock.timer.Start();
                }
                if (commands[0].Equals("homes", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.Homes.Dump();
                }
                if (commands[0].Equals("clock", StringComparison.CurrentCultureIgnoreCase))
                {
                    city.clock.Dump();
                }
                if (commands[0].Equals("roads", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (Block b in city.BlockMap)
                    {
                        city.printBlock(b);
                    }
                }
                if (cmd.Equals("print city"))
                {
                    city.printCity();
                }
                if (cmd.Equals("hour"))
                {
                    city.TickHour();
                }
                if (cmd.Equals("day"))
                {
                    city.TickDay();
                }
                if (cmd.Equals("week"))
                {
                    for (int i = 0; i < 7; i++)
                        city.TickDay();
                }
                if (cmd.Equals("month"))
                {
                    for (int i = 0; i < 30; i++)
                        city.TickDay();
                }
                if (cmd.Equals("year"))
                {
                    city.TickYear();
                }
                if (cmd.Equals("blocks"))
                {
                    int count = 0;
                    foreach (Block b in city.BlockMap)
                    {
                        count++;
                        Console.WriteLine(b.Type);
                    }
                    Console.WriteLine(count);
                }
                if (cmd.Equals("insert person"))
                {
                    MongoDAL dal = new MongoDAL();
                    dal.InsertPerson(city.AllPeople[0]);
                }
                if (cmd.Equals("save"))
                {
                    city.SaveState();
                }
                if (cmd.Equals("count"))
                {
                    city.PropertyCounts();
                }

            }
        }
    }

}

