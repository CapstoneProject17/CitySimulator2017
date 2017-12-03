using DBInterface.Econ;
using System;
using System.Collections.Generic;

namespace DBInterface.Infrastructure
{
    /// <summary>
    /// Industrial buildings are businesses which produce products, and
    /// and sells products to commercial buildings at a marked up price.
    /// <para>Written by Justin McLennan, Connor Goudie, Chandu Dissanayake, Andrew Busto 2017-10-02</para>
    /// <para>Last modified by Justin McLennan 2017-11-14</para>
    /// </summary>
    public class Industrial : Business
    {

        /// <summary>
        /// Default constructor for industrial buildings
        /// <para>Written by Justin McLennan, Andrew Busto 2017-10-02</para>
        /// <para>Last modified by Justin McLennan 2017-11-28</para>
        /// </summary>
        public Industrial() : base()
        {
            this.Type = "I";
        }

        /// <summary>
        /// Overloaded constructor for industrial buildings
        /// </summary>
        /// <para>Written by Justin McLennan, Connor Goudie 2017-11-08</para>
        /// <para>Last modified by Justin McLennan 2017-11-28</para>
        /// <param name="Name"></param>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Industrial(string Name, int capacity) : base(Name, capacity)
        {
            this.Type = "I";
            Inventory = 0;
            Market.IndustrialBusinesses.Add(this);

            //this command makes them hiring
            Market.BusinessesHiring.Add(this);
        }

        /// <summary>
        /// Generates more products each day
        /// <para>Written by Justin McLennan, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Justin McLennan 2017-11-28</para>
        /// </summary>
        public void CreateProducts()
        {
            Inventory += 200;
            Market.IndStock += 200;
        }

        /// <summary>
        /// Overriden method from business that calls create products
        /// <para>Written by Connor Goudie 2017-11-10</para>
        /// <para>Last modified by Justin McLennan 2017-11-28</para>
        /// </summary>
        public override void FillInventory()
        {
            CreateProducts();
        }
    }
}
