using System;
using System.Collections.Generic;
using ConsoleDump;
using ServerForTheLogic.Infrastructure;
using Newtonsoft.Json;
using ServerForTheLogic.Json;
using ServerForTheLogic.Econ;
using System.ServiceProcess;
using NLog;
using System.IO;
using System.Threading;
using CitySimNetworkService;

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


        public static void Start(string[] args)
        {

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
            string path = @"..\..\SerializedCity\city.json";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }

            // Open the file to read from.
            string readText = File.ReadAllText(path);
            city = null;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new LocationConverter());
            settings.Converters.Add(new BlockConverter());
            //JsonSerializer serializer = new JsonSerializer();
            city = JsonConvert.DeserializeObject<City>(readText, settings);

            if (city == null)
            {
                city = new City(fullUpdateQueue, partialUpdateQueue);
            }
            city.printCity();
            city.StartSimulation(fullUpdateQueue, partialUpdateQueue);
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
                if (cmd.Equals("print city"))
                {
                    city.printCity();
                }
            }
        }
    }

}

