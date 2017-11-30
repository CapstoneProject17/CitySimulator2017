using Newtonsoft.Json;
using DBInterface.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace DBInterface.Infrastructure
{
    /// <summary>
    /// Commercial buildings are businesses which purchase products from industrial buildings,
    /// and sell products to people at a marked up price.
    /// <para>Written by Chandu Dissanayake, Connor Goudie, Andrew Busto 2017-10-02</para>
    /// <para>Last modified by Andrew Busto 2017-11-14</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Commercial : Business
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
            Funds = 4000;
            Inventory = 0;
            this.Type = "C";
            Market.CommercialBusinesses.Add(this);
            Market.BusinessesHiring.Add(this);
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
                // Console.WriteLine("Sending order to market");
                Console.WriteLine(Name + " Bought: " + order.Amount + " C");
                Market.ProcessOrder(order, Market.IndustrialBusinesses);
                Market.ComStock += MINIMUM_VALUE;

            }

        }
    }
}
