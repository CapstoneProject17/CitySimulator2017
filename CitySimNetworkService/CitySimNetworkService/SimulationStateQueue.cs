using System.Collections.Concurrent;
using System.Json;
using System.Linq;

namespace CitySimNetworkService
{
    /// <summary>
    /// Holds a ConcurrentQueue for JSON objects, state buffer size, and functions to manage all previous.
    /// </summary>
    public class SimulationStateQueue
    {
        ConcurrentQueue<JsonObject> queue = new ConcurrentQueue<JsonObject>();
        private object lockObject = new object();

        public int StateBufferSize { get; set; }

        /// <summary>
        /// Enqueues a JSON object if possible.
        /// </summary>
        /// 
        /// <param name="Obj"> 
        /// JSON object to enqueue. 
        /// </param>
        public void Enqueue(JsonObject Obj)
        {
            queue.Enqueue(Obj);

            lock (lockObject)
            {
                while (queue.Count > StateBufferSize)
                {
                    queue.TryDequeue(out JsonObject overflow);
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
        /// JSON object containing partial state.
        /// </returns>
        public JsonObject GetPartialStateByID(int id)
        {
            var state = queue.Where((JsonObject s) => (s[id] == id)).FirstOrDefault();
            return state;
        }

        /// <summary>
        /// Returns a JSON object from start of the ConcurrentQueue, if successful.
        /// </summary>
        /// 
        /// <returns>
        /// JSON object from start of the ConcurrentQueue, if successful.
        /// </returns>
        public JsonObject Peek()
        {
            queue.TryPeek(out JsonObject result);
            return result;
        }
    }
}
