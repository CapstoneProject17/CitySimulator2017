using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
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
        public double WholesalePrice { get; set; }
        public double RetailPrice { get; set; }
        public static int GlobalCount { get; set; }
        public double Amount { get; set; }
        public Dictionary<Product,double> Materials { get; set; }

        /// <summary>
        /// Constructor #1. 
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ManufacturingPrice"></param>
        public Product (string ProductName, double ManufacturingPrice)
        {
            this.ProductName = ProductName;
        }

        /// <summary>
        /// Constructor #2. For industrial
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="ManufacturingPrice"></param>
        /// <param name="WholesalePrice"></param>
        public Product(string ProductName, double RetailPrice, double WholesalePrice)
        {
            this.ProductName = ProductName;
            this.RetailPrice = RetailPrice;
            this.WholesalePrice = WholesalePrice;
        }

    }
}
