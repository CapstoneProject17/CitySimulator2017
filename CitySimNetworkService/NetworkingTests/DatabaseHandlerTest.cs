using CitySimNetworkService;
using DataAccessLayer;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NetworkingTests
{
    /// <summary>
    /// Test class for DatabaseHandler - needs to be updated when server is running
    /// </summary>
    /// <author>
    /// Kevin
    /// </author>
    [TestClass]
    class DatabaseHandlerTest
    {
        DatabaseHandler dbHandler;
        RequestHandler requestHandler;
        AsyncServer connectionHandler;

        [ClassInitialize]
        public void ClassInit()
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

        /// <summary>
        /// Tests for a database object. Needs replacement for fake GUID
        /// </summary>
        [TestMethod]
        public void DbHandlerGridObjectTest()
        {
            string guid = new Guid().ToString();
            string sample = "{\"type\": \"database\", \"resourceID\": " + guid + "\"}";

            dynamic jsonObj = JsonConvert.DeserializeObject<DatabaseResourceRequest>(sample, new RequestJsonConverter());
            dynamic obj = dbHandler.HandleRequest(jsonObj);

            Assert.IsInstanceOfType(obj, typeof(ServerForTheLogic.ClientObject.GridObject));
        }

        [TestMethod]
        public void DbHandlerPersonTest()
        {
            string guid = new Guid().ToString();
            string sample = "{\"type\": \"database\", \"resourceID\": " + guid + "\"}";

            dynamic jsonObj = JsonConvert.DeserializeObject<DatabaseResourceRequest>(sample, new RequestJsonConverter());
            dynamic obj = dbHandler.HandleRequest(jsonObj);

            Assert.IsInstanceOfType(obj, typeof(ServerForTheLogic.ClientObject.Citizen));
        }

        [TestMethod]
        public void DbHandlerProductTest()
        {
            string guid = new Guid().ToString();
            string sample = "{\"type\": \"database\", \"resourceID\": " + guid + "\"}";

            dynamic jsonObj = JsonConvert.DeserializeObject<DatabaseResourceRequest>(sample, new RequestJsonConverter());
            dynamic obj = dbHandler.HandleRequest(jsonObj);

            Assert.IsInstanceOfType(obj, typeof(ServerForTheLogic.ClientObject.Citizen));
        }

        [TestMethod]
        public void DbHandlertTestFailure()
        {
            string guid = new Guid().ToString();
            string sample = "{\"type\": \"database\", \"resourceID\": " + guid + "\"}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());

            Assert.IsNotInstanceOfType(request, typeof(PartialSimulationUpdateRequest));
        }
    }
}
