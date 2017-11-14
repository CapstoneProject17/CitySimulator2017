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
        public Commercial() : base()
        {
            this.Type = "C";
            FillInventory();
        }
        public Commercial(string Name, int capacity,Boolean isTall) : base(Name, capacity,isTall)
        {
            this.Type = "C";
            FillInventory();
        }
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
                    Console.WriteLine("Bought " + order.Amount + " " + order.OrderProduct.ProductName);
                }

            }

            foreach (KeyValuePair<Product, int> p in productsBought)
            {
                inventory[p.Key] += p.Value;
            }
        }
    }
}
