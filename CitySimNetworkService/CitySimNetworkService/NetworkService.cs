﻿using Newtonsoft.Json;
using ServerForTheLogic;
using ServerForTheLogic.Json;
using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace CitySimNetworkService
{
    /// <summary>
    /// <Module>Networking Server Service</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Service initialization including server logic and networking</Description>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Setting up libraries required for service</ChangeLog>
    /// <Date>November 21, 2017</Date>
    /// </Author>
    /// </summary>
    public partial class NetworkService : ServiceBase
    {
        public const int FULL_QUEUE_SIZE = 1;
        public const int PARTIAL_UPDATE_QUEUE_SIZE = 50;
        private const int STANDARD_DEVIATION_DEATH = 14;
        private const int MEAN_DEATH_AGE = 80;

        /// <summary>
        /// Main connection handler.
        /// </summary>
        private AsyncServer connectionHandler;
        private City city;
        SimulationStateQueue fullUpdateQueue = new SimulationStateQueue
        {
            StateBufferSize = 1
        };
        SimulationStateQueue partialUpdateQueue = new SimulationStateQueue
        {
            StateBufferSize = 25
        };

        /// <summary>
        /// Initializes application network objects.
        /// </summary>
        public NetworkService()
        {
            InitializeComponent();
            SimulationStateHandler simulationHandler = new SimulationStateHandler(partialUpdateQueue, fullUpdateQueue);
            DatabaseHandler dbHandler = new DatabaseHandler();
            RequestHandler requestHandler = new RequestHandler(dbHandler, simulationHandler);
            connectionHandler = new AsyncServer(requestHandler);

            if (city == null)
            {
                city = new City(fullUpdateQueue, partialUpdateQueue);
            }
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
            city.InitSimulation(fullUpdateQueue, partialUpdateQueue);
            connectionHandler.StartListening();
        }

        /// <summary>
        /// Todos on service stop.
        /// </summary>
        protected override void OnStop()
        {
        }

        internal void TestStartAndStop(string[] args)
        {
            OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
