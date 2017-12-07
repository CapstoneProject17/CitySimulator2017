﻿using System.Collections.Generic;

namespace DALInterface.Econ
{
    /// <summary>
    /// A product that can be bought or sold
    /// Written by Chandu Dissanayake
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Variables
        /// </summary>
        public string ProductName { get; set; }
        public double ManufacturingPrice { get; set; }
        public double WholesalePrice { get; set; }
        public double RetailPrice { get; set; }
        public static int GlobalCount { get; set; }
        public Dictionary<Product,double> Materials { get; set; }

        /// <summary>
        /// Constructor #1. 
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ManufacturingPrice"></param>
        public Product (string ProductName, double ManufacturingPrice)
        {
            this.ProductName = ProductName;
            this.ManufacturingPrice = ManufacturingPrice;
        }

        /// <summary>
        /// Constructor #2. For industrial
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ManufacturingPrice"></param>
        /// <param name="WholesalePrice"></param>
        public Product(string ProductName, double ManufacturingPrice, double WholesalePrice)
        {
            this.ProductName = ProductName;
            this.ManufacturingPrice = ManufacturingPrice;
            this.WholesalePrice = WholesalePrice;
        }

        /// <summary>
        /// Constructor #3. For Commercial Stores
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ManufacturingPrice"></param>
        /// <param name="WholesalePrice"></param>
        /// <param name="RetailPrice"></param>
        public Product(string ProductName, double ManufacturingPrice, double WholesalePrice, double RetailPrice)
        {
            this.ProductName = ProductName;
            this.ManufacturingPrice = ManufacturingPrice;
            this.WholesalePrice = WholesalePrice;
            this.RetailPrice = RetailPrice;
        }

    }
}
