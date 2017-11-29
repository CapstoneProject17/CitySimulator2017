using DBInterface.Infrastructure;
using System.Collections.Generic;

namespace DBInterface.Econ
{
    public static class Market
    {
        //People will find a job here
        public static List<Business> BusinessesHiring { get; set; }
        //People will go here to buy products
        public static List<Business> CommercialBusinesses { get; set; }
        //Commercial will go here to buy products
        public static List<Business> IndustrialBusinesses { get; set; }
        //All business [maybe used for GDP calc]
        public static List<Business> Businesses { get; set; }
        //products Users would like to consume
        public static List<Product> ProductsInDemand { get; set; }
        public static List<Product> Products { get; set; }
        //public static List<Product> RawMaterials { get; set; }
        /// <summary>
        /// Initializing Market lists. Starting products initialized
        /// </summary>
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

        /// <summary>
        /// Method used by People and Commercial stores, to purchase items
        /// Called in Person.BuyThings() and Commercial.FillInventory()
        /// </summary>
        /// <para>Written by Chandu Dissanayake, Connor Goudie </para>
        /// <param name="order">Order object created</param>
        /// <param name="SellerList">Either list of Commercial buildings or list of Industrial buildings depending on who is purchasing</param>
        public static void ProcessOrder(Order order, List<Business> SellerList)
        {
            int quantityOrdered = order.Amount;
            ICustomer buyer = order.Sender;
            //Console.WriteLine("Businesses available " + SellerList.Count);
            int transfer;
            for (int i = 0; i < SellerList.Count && quantityOrdered > 0; ++i)
            {
                Business seller = SellerList[i];
                //If business carries product being ordered
                if (seller.inventory.ContainsKey(order.OrderProduct))
                {
                    int amountAvailable = seller.inventory[order.OrderProduct];
                    //if seller does not have enough to complete order.
                    if (amountAvailable < quantityOrdered)
                    {
                        transfer = (int)(order.OrderProduct.RetailPrice * amountAvailable);
                        seller.inventory[order.OrderProduct] -= amountAvailable;
                        //seller needs to order more
                        seller.FillInventory();
                        quantityOrdered -= amountAvailable;
                        //b.inventory[order.OrderProduct] -= amountAvailable;
                        seller.Funds += transfer;
                        buyer.Funds -= transfer;
                    }
                    //if seller can complete order
                    else if (amountAvailable >= quantityOrdered)
                    {
                        transfer = (int)(order.OrderProduct.RetailPrice * quantityOrdered);
                        seller.inventory[order.OrderProduct] -= quantityOrdered;
                        quantityOrdered = 0;
                        //b.inventory[order.OrderProduct] -= amountAvailable;
                        seller.Funds += transfer;
                        buyer.Funds -= transfer;


                    }
                }
            }

        }


    }
}
