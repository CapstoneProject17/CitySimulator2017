using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Commercial : Business
    {
        public const int MINIMUM_VALUE = 5;
        public Commercial() : base()
        {
            this.Type = "C";
        }
        public Commercial(string Name,int capacity) : base(Name,capacity)
        {
            this.Type = "C";
        }
        public override void BuyProducts()
        {
            foreach (KeyValuePair<Product, int> p in inventory)
            {

                if (p.Value < MINIMUM_VALUE)
                {
                    Order order = new Order(p.Key, 3 * MINIMUM_VALUE,this);
                    
                }

            }
        }

        public override void BuyProduct(Product product)
        {
            

        }
    }
}
