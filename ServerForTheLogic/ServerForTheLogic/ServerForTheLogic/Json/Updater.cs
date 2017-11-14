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
    class Updater<T>
    {
        /// <summary>
        /// Send a partial update from the simulator to clients. A partial update
        /// must consist of data that is not the entire state of the city.
        /// </summary>
        /// <param name="sendableData"></param>
        /// <param name="formatting"></param>
        public void sendPartialUpdate(T sendableData, Formatting formatting)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, formatting));
            Console.WriteLine(dataToSend.ToString());
            //Console.WriteLine(dataToSend.ToString().Length);
            //q.enqueue(dataToSend);
        }

        public void sendFullUpdate(T sendableData, Formatting formatting)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, formatting));
            //Console.WriteLine(dataToSend.ToString());
            //Console.WriteLine(dataToSend.ToString().Length);
            //q.enqueue(dataToSend);
        }
    }
}
