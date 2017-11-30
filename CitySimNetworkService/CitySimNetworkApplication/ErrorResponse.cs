using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkingApplication
{
    /// <summary>
    /// Holds the error that will be sent to client upon invalid request
    /// </summary>
    /// <author>
    /// Kevin
    /// </author>
    public class ErrorResponse
    {
        public string type = "error";
        public string Message { get; set; }
    }
}
