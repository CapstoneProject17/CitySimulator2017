using Newtonsoft.Json;
using DBInterface.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Default constructor for commercial buildings, fills inventory after initialization
        /// <para>Written by Chandu Dissanayake, Andrew Busto 2017-10-02</para>
        /// <para>Last modified by Andrew Busto 2017-11-14</para>
        /// </summary>
        public Commercial() : base()
        {
            this.Type = "C";
            FillInventory();
        }

        /// <summary>
        /// Overloaded constructor for commercial buildings, fills inventory after initialization
        /// </summary>
        /// <para>Written by Andrew Busto 2017-11-13</para>
        /// <param name="Name"></param>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Commercial(string Name, int capacity, Boolean isTall) : base(Name, capacity, isTall)
        {
            this.Type = "C";
            FillInventory();
        }

        /// <summary>
        /// Go through list of products and order more as needed.
        /// <para>Written by Chandu Dissanayake, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Chandu Dissanayake 2017-11-13</para>
        /// </summary>
        public override void FillInventory()
        {
            Dictionary<Product, int> productsBought = new Dictionary<Product, int>();
            foreach (KeyValuePair<Product, int> p in inventory)
            {
                if (p.Value < MINIMUM_VALUE)
                {
                    Order order = new Order(p.Key, MINIMUM_VALUE, this);
                    productsBought.Add(p.Key, order.Amount);
                    // Console.WriteLine("Sending order to market");
                    Market.ProcessOrder(order, Market.IndustrialBusinesses);
                    //Console.WriteLine("Bought " + order.Amount + " " + order.OrderProduct.ProductName);
                }

            }

            foreach (KeyValuePair<Product, int> p in productsBought)
            {
                inventory[p.Key] += p.Value;
            }
        }
    }
}
