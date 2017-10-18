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
    class Updater
    {
        public void sendHourlyUpdate(Dictionary<Guid, Point> dictionary)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(dictionary, Formatting.None));
            //q.enqueue(dataToSend);
        }

        public void sendDailyUpdateo(object[] sendableData)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, Formatting.None));
            //q.enqueue(dataToSend);
        }

        public void sendWeeklyUpdate(object[] sendableData)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, Formatting.None));
            //q.enqueue(dataToSend);
        }

        public void sendFullUpdate(object[] sendableData)
        {
            //Queue q = new Queue();
            JObject dataToSend = JObject.Parse(JsonConvert.SerializeObject(sendableData, Formatting.None));
            //q.enqueue(dataToSend);
        }
    }
}
