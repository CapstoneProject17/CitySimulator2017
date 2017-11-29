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
        //Products currently generated and sold to other businesses
        public Dictionary<Product, int> Supplies { get; set; }

        /// <summary>
        /// Default constructor for industrial buildings
        /// <para>Written by Justin McLennan, Andrew Busto 2017-10-02</para>
        /// <para>Last modified by Justin McLennan 2017-11-12</para>
        /// </summary>
        public Industrial() : base()
        {
            this.Type = "I";
            Supplies = new Dictionary<Product, int>();
            CreateProducts();
        }

        /// <summary>
        /// Overloaded constructor for industrial buildings
        /// </summary>
        /// <para>Written by Justin McLennan, Connor Goudie 2017-11-08</para>
        /// <para>Last modified by Justin McLennan 2017-11-14</para>
        /// <param name="Name"></param>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Industrial(string Name, int capacity, Boolean isTall) : base(Name, capacity, isTall)
        {
            this.Type = "I";
            Supplies = new Dictionary<Product, int>();
            Market.IndustrialBusinesses.Add(this);
            Market.BusinessesHiring.Add(this);
            CreateProducts();
        }

        /// <summary>
        /// Generates more products each day
        /// <para>Written by Justin McLennan, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Justin McLennan 2017-11-14</para>
        /// </summary>
        public void CreateProducts()
        {
            int NumProductsPerDay = 0;
            List<Product> productsNeeded = new List<Product>();

            foreach (KeyValuePair<Product, int> p in inventory)
            {
                //if (p.Value < MINIMUM_VALUE)
                //{
                productsNeeded.Add(p.Key);
                //}

            }

            foreach (Product p in productsNeeded)
            {
                NumProductsPerDay = 10;//(int)((workers.Count / p.ManufacturingPrice) * p.WholesalePrice);
                inventory[p] += NumProductsPerDay;
                Funds -= (int)(NumProductsPerDay * p.ManufacturingPrice);
            }
        }

        /// <summary>
        /// Overriden method from business that calls create products
        /// <para>Written by Connor Goudie 2017-11-10</para>
        /// </summary>
        public override void FillInventory()
        {
            CreateProducts();
        }

        /// <summary>
        /// Generate more of a sinle product, if it exists in current inventory.
        /// <para>Written by Andrew Busto 2017-11-08</para>
        /// </summary>
        /// <param name="product"></param>
        public void CreateProduct(Product product)
        {
            if (inventory.ContainsKey(product))
            {
                int NumProductsPerDay = (int)((workers.Count / product.ManufacturingPrice) * product.WholesalePrice);
                inventory[product] += NumProductsPerDay;
                Funds -= (int)(NumProductsPerDay * product.ManufacturingPrice);
            }
        }
    }
}
