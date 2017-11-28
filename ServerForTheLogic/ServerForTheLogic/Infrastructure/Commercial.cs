using Newtonsoft.Json;
using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    /// <summary>
    /// Commercial buildings are businesses which purchase products from industrial buildings,
    /// and sell products to people at a marked up price.
    /// <para>Written by Chandu Dissanayake, Connor Goudie, Andrew Busto 2017-10-02</para>
    /// <para>Last modified by Andrew Busto 2017-11-14</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    class Commercial : Business
    {
        /// <summary>
        /// Default constructor for commercial buildings, fills Inventory after initialization
        /// <para>Written by Chandu Dissanayake, Andrew Busto 2017-10-02</para>
        /// <para>Last modified by Andrew Busto 2017-11-14</para>
        /// </summary>
        public Commercial() : base()
        {
            this.Type = "C";
        }

        /// <summary>
        /// Overloaded constructor for commercial buildings, fills Inventory after initialization
        /// </summary>
        /// <para>Written by Andrew Busto 2017-11-13</para>
        /// <param name="Name"></param>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Commercial(string Name, int capacity, Boolean isTall) : base(Name, capacity, isTall)
        {
            this.Type = "C";
            Market.CommercialBusinesses.Add(this);
            //Market.BusinessesHiring.Add(this);
        }

        /// <summary>
        /// Go through list of products and order more as needed.
        /// <para>Written by Chandu Dissanayake, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Chandu Dissanayake 2017-11-13</para>
        /// </summary>
        public override void FillInventory()
        {
            int rand = new Randomizer().Number(0, Market.Products.Count - 1);

            if (Inventory < MINIMUM_VALUE)
            {
                Order order = new Order(Market.Products[rand], MINIMUM_VALUE, this);
                Market.ComStock += MINIMUM_VALUE;
                Market.IndStock -= MINIMUM_VALUE;
                // Console.WriteLine("Sending order to market");
                Market.ProcessOrder(order, Market.IndustrialBusinesses);
                //Console.WriteLine("Bought " + order.Amount + " " + order.OrderProduct.ProductName);
            }

        }
    }
}
