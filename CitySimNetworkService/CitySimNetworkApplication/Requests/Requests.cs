using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    /// <summary>
    /// <Module>Networking Server Connection</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Different request types for the request handlers</Description>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Setting up the request types</ChangeLog>
    /// <Date>November 01, 2017</Date>
    /// </Author>
    /// </summary>
    public abstract class BaseRequest 
    {
        public string RequestType { get; set; }
    }

    /// <summary>
    /// Contains update type and update measure (full or partial).
    /// </summary>
    public class SimulationUpdateRequest: BaseRequest
    {
        public string UpdateType { get; set; }
        public bool FullUpdate { get; set; }
    }

    /// <summary>
    /// Contains last update information.
    /// </summary>
    public class PartialSimulationUpdateRequest: SimulationUpdateRequest 
    {
        public int LastUpdate { get; set; }
    }

    /// <summary>
    /// Contains resource type and resource id.
    /// </summary>
    public class DatabaseResourceRequest: BaseRequest
    {
        public string ResourceType { get; set; }
        public string ResourceID { get; set; }
    } 
}
