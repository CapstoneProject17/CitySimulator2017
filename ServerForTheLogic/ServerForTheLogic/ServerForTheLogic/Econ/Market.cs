using ServerForTheLogic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    static class Market
    {
        public static List<Business> BusinessesHiring { get; set; }
        //People will go here to buy products
        public static List<Commercial> CommercialBusinesses { get; set; }
        //Commercial will go here to buy products
        public static List<Industrial> IndustrialBusinesses { get; set; }
        public static List<Business> Businesses { get; set; }
        public static List<Product> ProductsInDemand { get; set; }
        public static List<Product> Products { get; set; }
        //public static List<Product> RawMaterials { get; set; }
        static Market ()
        {
            
        
            ProductsInDemand = new List<Product>();
            Products = new List<Product>();
            Businesses = new List<Business>();
            IndustrialBusinesses = new List<Industrial>();
            BusinessesHiring = new List<Business>();
            CommercialBusinesses = new List<Commercial>();
           // RawMaterials = new List<Product>();

            Product meme = new Product("Meme", 10,20,30);
            //RawMaterials.Add(meme);
            ProductsInDemand.Add(meme);
            Products.Add(meme);


            //RawMaterials.Add(new Product("Metal", 20));
            //RawMaterials.Add(new Product("Cotton", 10));
            //RawMaterials.Add(new Product("Water", 10));
            //RawMaterials.Add(new Product("Silk", 10));
            //RawMaterials.Add(new Product("Plastic", 10));

            //ProductInDemand = 
        }

        public static void ProcessCustOrder(Order order)
        {
            int quantity = order.Amount;
            Person customer = (Person)order.Sender;

            foreach (Commercial c in CommercialBusinesses)
            {
                foreach (KeyValuePair<Product, int> p in c.inventory)
                {
                    // do something with entry.Value or entry.Key
                    if (p.Key.Equals(order.OrderProduct))
                    {
                        if (p.Value >= quantity)
                        {
                            c.inventory[p.Key] -= quantity;
                            customer.Money -= (int)(p.Key.RetailPrice * quantity);
                            c.Funds += (int)(p.Key.RetailPrice * quantity);
                            break;
                        } 
                        else
                        {
                            quantity -= c.inventory[p.Key];
                            customer.Money -= (int)(p.Key.RetailPrice * c.inventory[p.Key]);
                            c.Funds += (int)(p.Key.RetailPrice * c.inventory[p.Key]);
                            c.inventory[p.Key] = 0 ;
                            //STOCK UP INVENTORY

                        }

                    }
                }
            }
        }

        public static void ProcessBusOrder(Order order)
        {
            int quantity = order.Amount;
            Business customer = (Business)order.Sender;

            foreach (Industrial i in IndustrialBusinesses)
            {
                foreach (KeyValuePair<Product, int> p in i.inventory)
                {
                    // do something with entry.Value or entry.Key
                    if (p.Key.Equals(order.OrderProduct))
                    {
                        if (p.Value >= quantity)
                        {
                            i.inventory[p.Key] -= quantity;

                            i.Funds += (int)(p.Key.RetailPrice * quantity);
                            break;
                        }
                        else
                        {
                            quantity -= i.inventory[p.Key];
                            i.Funds += (int)(p.Key.RetailPrice * i.inventory[p.Key]);
                            i.inventory[p.Key] = 0;
                            //PRODUCE MORE
                            i.CreateProduct(p.Key);

                        }



                    }
                }
            }
        }



    }
}
