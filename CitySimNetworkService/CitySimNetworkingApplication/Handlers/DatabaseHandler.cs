using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CitySimNetworkService
{
	/// <summary> 
	/// Contains handler for database request. 
	/// </summary>
	/// 
	/// <author> 
	/// Harman Mahal, Kevin Mitchell
	/// </author>
	internal class DatabaseHandler
	{
		/// <summary> 
		/// Handler; returns JSON object containing Object (Buildings/Citizens) information on valid request. 
		/// </summary>
		/// 
		/// <param name="request">
		/// Contains resource type and resource id. 
		/// </param>
		/// 
		/// <returns> 
		/// JSON object; has Buildings/Citizens/empty information or error info. 
		/// </returns>
		internal string HandleRequest(DatabaseResourceRequest request)
		{

			switch (request.ResourceType)
			{
				case "object":
					Guid guid = new Guid(request.ResourceID);   //parse resourceID into GUID
					Object obj = GetObjectById(guid);           //get object from database via GUID
					return JsonConvert.SerializeObject(obj);    //serialize object as JSON and return to client

				default:
					//invalid request returns an "invalid request" JSON
					string errorString = "{Request: 'invalid'}";
					JObject errorJSON = JObject.Parse(errorString);
					return "";
			}
		}

		//METHOD STUB TO REPLACE ON INTEGRATION
		public Object GetObjectById(Guid guid)
		{
			return new object();
		}
	}
}