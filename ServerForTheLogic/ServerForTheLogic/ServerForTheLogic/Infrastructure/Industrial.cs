using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
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

        }
        public Industrial(string Name, int capacity) : base(Name,capacity)
        {
            this.Type = "I";
            Supplies = new Dictionary<Product, int>();

        }

        /// <summary>
        /// Create products per day.
        /// </summary>
        public void CreateProducts()
        {
            int NumProductsPerDay;

            foreach (KeyValuePair<Product, int> p in inventory)
            {
                NumProductsPerDay =(int)( (workers.Count / p.Key.ManufacturingPrice) * p.Key.WholesalePrice);
                inventory[p.Key] += NumProductsPerDay;
                Funds -=(int) (NumProductsPerDay * p.Key.ManufacturingPrice);
            }
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
