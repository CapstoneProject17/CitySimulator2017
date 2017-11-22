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

        /// <summary>
        /// Main connection handler.
        /// </summary>
        private AsyncServer connectionHandler;

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
        }

        /// <summary>
        /// Todos on service stop.
        /// </summary>
        protected override void OnStop()
        {
        }
    }
}
