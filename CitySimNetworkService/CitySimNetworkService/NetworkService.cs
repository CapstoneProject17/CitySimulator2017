using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    public partial class NetworkService : ServiceBase
    {
        public const int FULL_QUEUE_SIZE = 1;
        public const int PARTIAL_UPDATE_QUEUE_SIZE = 25;

        private AsyncServer connectionHandler;

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

        protected override void OnStart(string[] args)
        {
            connectionHandler.StartListening();
        }

        protected override void OnStop()
        {
        }
    }
}
