using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    static class Program
    {
        /// <summary>
        /// <Module>Networking Server Connection</Module>
        /// <Team>Networking Team</Team>
        /// <Description>The main entry point for networking application</Description>
        /// <Author>
        /// <By>Harman Mahal</By>
        /// <ChangeLog>Initial server set up</ChangeLog>
        /// <Date>November 04, 2017</Date>
        /// </Author>
        /// </summary>

        static void Main(string[] args)
        {
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
            AsyncServer connectionHandler = new AsyncServer(requestHandler);
            connectionHandler.StartListening();
        }
    }
}
