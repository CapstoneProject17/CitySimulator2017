using CitySimNetworkService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Request.Test
{
    /// <summary>
    /// Contains tests for the request handler.
    /// </summary>
    /// 
    /// <author>
    /// Francis Carreon
    /// </author>
    [TestClass]
    public class RequestTests
    {
        /// <summary>
        /// Checks if the string object deserializes to a DRR object.
        /// </summary>
        [TestMethod]
        public void DbRequestTest()
        {
            string  sample  = "{type:database}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());
            
            Assert.IsInstanceOfType(request, typeof(DatabaseResourceRequest));
        }

        /// <summary>
        /// Checks if the string object deserializes to a PSUR object.
        /// </summary>
        [TestMethod]
        public void SimPartRequestTest()
        {
            string  sample  = "{type:update,FullUpdate:false}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());

            Assert.IsInstanceOfType(request, typeof(PartialSimulationUpdateRequest));
        }

        /// <summary>
        /// Checks if the string object deserializes to an SUR object.
        /// </summary>
        [TestMethod]
        public void SimRequestTest()
        {
            string  sample  = "{type:update,FullUpdate:true}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());

            Assert.IsInstanceOfType(request, typeof(SimulationUpdateRequest));
        }

    }
}
