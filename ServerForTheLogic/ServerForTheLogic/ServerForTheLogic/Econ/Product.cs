using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    class Product
    {
        /// <summary>
        /// Variables
        /// </summary>
        public string ProductName { get; set; }
        public double ManufacturingPrice { get; set; }
        public double WholesalePrice { get; set; }
        public double RetailPrice { get; set; }
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
