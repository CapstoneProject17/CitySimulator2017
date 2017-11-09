using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Industrial : Business
    {
        public Dictionary<Product,int > Supplies { get; set; }

        public Industrial() : base()
        {
            
            this.Type = "I";
            Supplies = new Dictionary<Product, int>();
            CreateProducts();
        }
        public Industrial(string Name, int capacity) : base(Name,capacity)
        {
            this.Type = "I";
            Supplies = new Dictionary<Product, int>();
            CreateProducts();
        }

        /// <summary>
        /// Create products per day.
        /// </summary>
        public void CreateProducts()
        {
            int NumProductsPerDay=0;
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

        public override void FillInventory()
        {
            CreateProducts();
        }
        public void CreateProduct(Product product)
        {
            if (inventory.ContainsKey(product))
            {
                int NumProductsPerDay = (int)((workers.Count / product.ManufacturingPrice) * product.WholesalePrice);
                inventory[product] += NumProductsPerDay;
                Funds -= (int)(NumProductsPerDay * product.ManufacturingPrice);
            }

        }

        public override void AddProductToInventory()
        {
            base.AddProductToInventory();
        }
    }
}
