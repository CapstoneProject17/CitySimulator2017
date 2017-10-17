using System.Collections.Concurrent;
using System.Json;
using System.Linq;

namespace CitySimNetworkService
{
    public class SimulationStateQueue
    {
        ConcurrentQueue<JsonObject> queue = new ConcurrentQueue<JsonObject>();
        private object lockObject = new object();

        public int StateBufferSize { get; set; }

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

        public JsonObject GetPartialStateByID(int id)
        {
            var state = queue.Where((JsonObject s) => (s[id] == id)).FirstOrDefault();
            return state;
        }

        public JsonObject Peek()
        {
            queue.TryPeek(out JsonObject result);
            return result;
        }
    }
}
