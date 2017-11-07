using CitySimNetworkService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Request.Test
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void DbRequestTest()
        {
            string  sample  = "{type:database}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());
            
            Assert.IsInstanceOfType(request, typeof(DatabaseResourceRequest));
        }

        [TestMethod]
        public void SimPartRequestTest()
        {
            string  sample  = "{type:update,FullUpdate:false}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());

            Assert.IsInstanceOfType(request, typeof(PartialSimulationUpdateRequest));
        }

        [TestMethod]
        public void SimRequestTest()
        {
            string  sample  = "{type:update,FullUpdate:true}";

            dynamic request = JsonConvert.DeserializeObject<BaseRequest>(sample, new RequestJsonConverter());

            Assert.IsInstanceOfType(request, typeof(SimulationUpdateRequest));
        }

    }
}
