﻿using CitySimNetworkingApplication;
using Newtonsoft.Json;
using NLog;
using System;

namespace CitySimNetworkService
{
    /// <summary> 
    /// Contains a function to specify database and simulation handlers, and another to parse requests.
    /// </summary>
    /// <author>
    /// Harman Mahal
    /// </author>
    public class RequestHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private DatabaseHandler dbHandler;
        private SimulationStateHandler simulationStateHandler;

        /// <summary>
        /// Specifies application handlers.
        /// </summary>
        /// 
        /// <param name="_dbHandler">
        /// Database handler to set.
        /// </param>
        /// 
        /// <param name="_simHandler">
        /// Simulation handler to set.
        /// </param>
        public RequestHandler(DatabaseHandler _dbHandler, SimulationStateHandler _simHandler)
        {
            dbHandler = _dbHandler;
            simulationStateHandler = _simHandler;
        }

        /// <summary>
        /// Parses string request to proper JSON object type.
        /// </summary>
        /// 
        /// <param name="_request">
        /// String request to parse.
        /// </param>
        /// 
        /// <returns> 
        /// JSON object of some type (Object, Product, Citizen, Error)
        /// </returns>
        public string ParseRequest(string _request)
        {
            try
            {
                BaseRequest request = JsonConvert.DeserializeObject<BaseRequest>(_request, new RequestJsonConverter());
                switch (request.RequestType)
                {
                    case "update":
                        return simulationStateHandler.HandleUpdateRequest((SimulationUpdateRequest)request);
                    case "database":
                        return dbHandler.HandleRequest((DatabaseResourceRequest)request);
                    default:
                        logger.Error("Invalid request made: {0}", _request);
						return JsonConvert.SerializeObject(
							new ErrorResponse { Message = "Invalid Request: Malformed Request" });
                }
            } catch(Exception e)
            {
                logger.Error(e);
				ErrorResponse error = new ErrorResponse
				{
					Message = "Invalid Request: Unhandled Exception"
				};
                return JsonConvert.SerializeObject(error);
            }
       }
    }
}