﻿using System;
using Newtonsoft.Json;
using DataAccessLayer;
using CitySimNetworkingApplication;

namespace CitySimNetworkService
{
    /// <summary> 
    /// Contains handler for database request. 
    /// </summary>
    /// 
    /// <author> 
    /// Harman Mahal, Kevin Mitchell
    /// </author>
    public class DatabaseHandler
	{
		/// <summary>
		/// MongoDAL used to call Database methods
		/// </summary>
		MongoDAL data;

		/// <summary>
		/// DatabaseHandler is instantiated in Program
		/// </summary>
		public DatabaseHandler()
		{
			data = new MongoDAL();
		}

		/// <summary> 
		/// Handler; returns JSON object containing Buildings/Citizens information on valid request. 
		/// </summary>
		/// 
		/// <param name="request">
		/// Contains resource type and resource id. 
		/// </param>
		/// 
		/// <returns> 
		/// JSON object; has Buildings/Citizens/empty information. 
		/// </returns>
		public string HandleRequest(DatabaseResourceRequest request)
		{
			try
			{
				switch (request.ResourceType)
				{
					case "object":
						Guid guid = new Guid(request.ResourceID);   //parse resourceID into GUID
						Object obj = data.GetObjectByGuid(guid);    //get object from database via GUID
						return JsonConvert.SerializeObject(obj);    //serialize object as JSON and return to client

					case "product":
						Object product = data.GetProduct(request.ResourceID);
						return JsonConvert.SerializeObject(product);

					case "clock":
						Object clock = data.GetClock();
						return JsonConvert.SerializeObject(clock);

					default:
						ErrorResponse error = new ErrorResponse { Message = "Invalid Database Request: invalid database request type" };
						return JsonConvert.SerializeObject(error);
				}
			} catch (Exception)
			{
				ErrorResponse error = new ErrorResponse { Message = "Invalid Database Request: GUID not found" };
				return JsonConvert.SerializeObject(error);
			}
		}
	}
}