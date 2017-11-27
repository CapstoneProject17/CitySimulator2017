using Newtonsoft.Json;
using ServerForTheLogic;
using ServerForTheLogic.Json;
using System.Collections.Generic;
using System.ServiceProcess;

namespace CitySimNetworkService
{
    /// <summary>
    /// Has application network features.
    /// </summary>
    public partial class NetworkService : ServiceBase
    {
        public const int FULL_QUEUE_SIZE = 1;
        public const int PARTIAL_UPDATE_QUEUE_SIZE = 25;
        private const int STANDARD_DEVIATION_DEATH = 14;
        private const int MEAN_DEATH_AGE = 80;

        /// <summary>
        /// Main connection handler.
        /// </summary>
        private AsyncServer connectionHandler;
        private City city;
        private Updater<City> updater;

        /// <summary>
        /// Initializes application network objects.
        /// </summary>
        public NetworkService()
        {
            InitializeComponent();
            SimulationStateQueue fullUpdateQueue = new SimulationStateQueue
            {
                StateBufferSize = 1
            };
            SimulationStateQueue partialUpdateQueue = new SimulationStateQueue
            {
                StateBufferSize = 25
            };

            SimulationStateHandler simulationHandler = new SimulationStateHandler(partialUpdateQueue, fullUpdateQueue);
            DatabaseHandler dbHandler = new DatabaseHandler();
            RequestHandler requestHandler = new RequestHandler(dbHandler, simulationHandler);
            connectionHandler = new AsyncServer(requestHandler);

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
            updater = new Updater<City>(partialUpdateQueue, fullUpdateQueue);
        }

        /// <summary>
        /// Todos on service start.
        /// </summary>
        /// 
        /// <param name="args">
        /// Unused.
        /// </param>
        protected override void OnStart(string[] args)
        {
            connectionHandler.StartListening();
            updater.sendFullUpdate(city, Formatting.Indented);
        }

        /// <summary>
        /// Todos on service stop.
        /// </summary>
        protected override void OnStop()
        {
        }
    }
}
