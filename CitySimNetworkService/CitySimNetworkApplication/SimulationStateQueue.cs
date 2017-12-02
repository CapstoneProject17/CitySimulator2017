using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using CitySimNetworkingApplication;

namespace CitySimNetworkService
{
    /// <summary>
    /// </summary>
    /// <summary>
    /// <Module>Networking Server Connection</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Holds a ConcurrentQueue for JSON objects, state buffer size, and functions to manage all previous.</Description>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Setting up the simulation handler queue</ChangeLog>
    /// <Date>November 01, 2017</Date>
    /// </Author>
    /// </summary>
    public class SimulationStateQueue
    {
        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private object lockObject = new object();

        public int StateBufferSize { get; set; }

        /// <summary>
        /// Enqueues a JSON object if possible.
        /// </summary>
        /// 
        /// <param name="Obj"> 
        /// JSON object to enqueue. 
        /// </param>
        public void Enqueue(string Obj)
        {
            queue.Enqueue(Obj);

            lock (lockObject)
            {
                while (queue.Count > StateBufferSize)
                {
                    queue.TryDequeue(out string overflow);
                }
            }
        }

        /// <summary>
        /// Returns a JSON object containing partial state.
        /// </summary>
        /// 
        /// <param name="id">
        /// Specific id to find.
        /// </param>
        /// 
        /// <returns>
        /// JSON object containing partial state or error
        /// </returns>
        public string GetPartialStateByID(int id)
        {
            lock (lockObject)
            {
                foreach (string s in queue)
                {
                    JObject o = JObject.Parse(s);
                    if((int)o["NetHours"] == id)
                    {
                        return s;
                    }
                }
                return JsonConvert.SerializeObject(new ErrorResponse { Message = "Update not found" });
            }
        }

        /// <summary>
        /// Returns a JSON object from start of the ConcurrentQueue, if successful.
        /// </summary>
        /// 
        /// <returns>
        /// JSON object from start of the ConcurrentQueue, if successful.
        /// </returns>
        public string Peek()
        {
            queue.TryPeek(out string result);
            return result;
        }
    }
}
