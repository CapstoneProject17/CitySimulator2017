using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    public abstract class BaseRequest 
    {
        public string RequestType { get; set; }
    }

    public class SimulationUpdateRequest: BaseRequest
    {
        public string UpdateType { get; set; }
        public bool FullUpdate { get; set; }
    }

    public class PartialSimulationUpdateRequest: SimulationUpdateRequest 
    {
        public int LastUpdate { get; set; }
    }

    public class DatabaseResourceRequest: BaseRequest
    {
        public string ResourceType { get; set; }
        public string ResourceID { get; set; }

    } 
}
