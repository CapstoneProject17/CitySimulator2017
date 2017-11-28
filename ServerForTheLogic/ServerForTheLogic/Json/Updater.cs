using CitySimNetworkService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Json
{

    /// <summary>
    /// Updater takes in a generic type of object, and queues it to be
    /// sent accross the network to clients or database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Updater<T>
    {

        private SimulationStateQueue partialUpdateQueue;
        private SimulationStateQueue fullUpdateQueue;

        public Updater(SimulationStateQueue _full, SimulationStateQueue _partial)
        {
            partialUpdateQueue = _partial;
            fullUpdateQueue = _full;
        }


        /// <summary>
        /// Send a partial update from the simulator to clients. A partial update
        /// must consist of data that is not the entire state of the city.
        /// </summary>
        /// <param name="sendableData"></param>
        /// <param name="formatting"></param>
        public void SendPartialUpdate(T sendableData, Formatting formatting)
        {
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, formatting));
            //Console.WriteLine(dataToSend.ToString());
            //Console.WriteLine(dataToSend.ToString().Length);
            partialUpdateQueue.Enqueue(dataToSend.ToString());
        }

        /// <summary>
        /// Send the entire state of the city to the db.
        /// </summary>
        /// <param name="sendableData"> The data to be serealized. </param>
        /// <param name="formatting"> The formatting rules to be followed in serealization. </param>
        /// <returns> The serialized Json string (for testing) </returns>
        public void SendFullUpdate(T sendableData, Formatting formatting)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new LocationConverter());
            string dataToSend = JsonConvert.SerializeObject(sendableData, settings);
            //System.IO.File.WriteAllText(@"..\..\SerializedCity\json.txt", dataToSend.ToString());
            fullUpdateQueue.Enqueue(dataToSend);
        }

        /// <summary>
        /// Saves the city state to a file, so it can be loaded from the backup later.
        /// </summary>
        /// <param name="sendableData"></param>
        public void SaveCityState(T sendableData)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new LocationConverter());
            string dataToSend = JsonConvert.SerializeObject(sendableData, settings);
            System.IO.File.WriteAllText(@"..\..\SerializedCity\city.json", dataToSend);
        }
    }
}
