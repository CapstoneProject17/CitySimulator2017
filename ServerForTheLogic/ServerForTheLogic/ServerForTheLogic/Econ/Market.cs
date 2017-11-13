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
        public static List<Business> CommercialBusinesses { get; set; }
        //Commercial will go here to buy products
        public static List<Business> IndustrialBusinesses { get; set; }
        public static List<Business> Businesses { get; set; }
        public static List<Product> ProductsInDemand { get; set; }
        public static List<Product> Products { get; set; }
        //public static List<Product> RawMaterials { get; set; }
        static Market()
        {
            ProductsInDemand = new List<Product>();
            Products = new List<Product>();
            Businesses = new List<Business>();
            IndustrialBusinesses = new List<Business>();
            BusinessesHiring = new List<Business>();
            CommercialBusinesses = new List<Business>();
            // RawMaterials = new List<Product>();

            Product meme = new Product("Meme", 10, 20, 30);
            //RawMaterials.Add(meme);
            ProductsInDemand.Add(meme);
            Products.Add(meme);

            //RawMaterials.Add(new Product("Metal", 20));
            //RawMaterials.Add(new Product("Cotton", 10));
            //RawMaterials.Add(new Product("Water", 10));
            //RawMaterials.Add(new Product("Silk", 10));
            //RawMaterials.Add(new Product("Plastic", 10));
        }

        public static void ProcessOrder(Order order, List<Business> SellerList)
        {
            int quantityOrdered = order.Amount;
            ICustomer buyer = order.Sender;
            List<Business> storesAvailable = new List<Business>();
            Console.WriteLine("Businesses available " + SellerList.Count);
            int transfer;
            for (int i = 0; i < SellerList.Count && quantityOrdered > 0; ++i)
            {
                Business seller = SellerList[i];
                if (seller.inventory.ContainsKey(order.OrderProduct))
                {
                    int amountAvailable = seller.inventory[order.OrderProduct];
                    if (amountAvailable < quantityOrdered && amountAvailable != 0)
                    {
                        //storesAvailable.Add(b);
                        transfer = (int)(order.OrderProduct.RetailPrice * amountAvailable);
                        seller.inventory[order.OrderProduct] -= amountAvailable;
                        //seller needs to order more
                        seller.FillInventory();
                        quantityOrdered -= amountAvailable;
                        //b.inventory[order.OrderProduct] -= amountAvailable;
                        seller.Funds += transfer;
                        buyer.Funds -= transfer;
                    }
                    else if (amountAvailable >= quantityOrdered)
                    {
                        //storesAvailable.Add(b);
                        transfer = (int)(order.OrderProduct.RetailPrice * quantityOrdered);
                        seller.inventory[order.OrderProduct] -= quantityOrdered;
                    
                        quantityOrdered = 0;
                        //b.inventory[order.OrderProduct] -= amountAvailable;
                        seller.Funds += transfer;
                        buyer.Funds -= transfer;

                    }
                }
            }
            Console.WriteLine("Available count = " + storesAvailable.Count);
            //int transfer;
            /*for (int i = 0; i < storesAvailable.Count; ++i)
            {
                if (i == storesAvailable.Count - 1)
                {
                    transfer = (int)(order.OrderProduct.RetailPrice * quantity);
                    Console.WriteLine("tf" + transfer);
                    Console.WriteLine("cnt" + storesAvailable[i].inventory[order.OrderProduct]);
                    storesAvailable[i].inventory[order.OrderProduct] -= quantity;
                    customer.Funds -= transfer;
                    Console.WriteLine("customer funds = " + customer.Funds);
                    Console.WriteLine("store available = " + storesAvailable[i].Funds);
                    storesAvailable[i].Funds += transfer;
                    Console.WriteLine(storesAvailable[i].Funds);
                }
                else
                {
                   // quantity -= storesAvailable[i].inventory[order.OrderProduct];
                    transfer = (int)(order.OrderProduct.RetailPrice * storesAvailable[i].inventory[order.OrderProduct]);
                    customer.Funds -= transfer;
                    storesAvailable[i].Funds += transfer;
                    storesAvailable[i].inventory[order.OrderProduct] = 0;
                    //ORDER MORE PRODUCT
                    storesAvailable[i].FillInventory();
                }*/
         
        }
    }
}
