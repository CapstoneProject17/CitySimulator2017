using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkingApplication
{
	/// <summary>
	/// Holds the error that will be sent to client upon invalid requests
	/// </summary>
	/// <author>
	/// Kevin
	/// </author>
	public class ErrorResponse
	{
		/// <summary>
		/// Http response type: error/data/etc
		/// </summary>
		public string type = "error";

		/// <summary>
		/// Message that the Http Error Response will contain
		/// </summary>
		public string Message { get; set; }
	}
}
