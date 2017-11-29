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
        //People will find a job here
        public static List<Business> BusinessesHiring { get; set; }
        //People will go here to buy products
        public static List<Business> CommercialBusinesses { get; set; }
        //Commercial will go here to buy products
        public static List<Business> IndustrialBusinesses { get; set; }
        //All business [maybe used for GDP calc]
        public static List<Business> Businesses { get; set; }
        //products Users would like to consume
        public static int ComStock { get; set; }
        public static int IndStock { get; set; }
        public static List<Product> Products { get; set; }
        //public static List<Product> RawMaterials { get; set; }
        /// <summary>
        /// Initializing Market lists. Starting products initialized
        /// </summary>
        static Market()
        {

            Products = new List<Product>();
            Businesses = new List<Business>();
            IndustrialBusinesses = new List<Business>();
            BusinessesHiring = new List<Business>();
            CommercialBusinesses = new List<Business>();
            Product Life = new Product("Life", 30, 20);
            Products.Add(Life);
            ComStock = 0;
            IndStock = 0;

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
            int NumOrdered = order.Amount;
            ICustomer buyer = order.Buyer;
            int NumofSellers = SellerList.Count;
            Business first = SellerList.First();
            double NumPerSeller = NumOrdered / NumofSellers;


            if (first.Type.Equals("C", StringComparison.CurrentCultureIgnoreCase))
            {
                if(Market.ComStock < NumOrdered)
                {
                    foreach (Business b in SellerList)
                    {
                        b.FillInventory();
                    }
                }
                Market.ComStock -= NumOrdered;
                buyer.Funds -= NumOrdered * order.OrderProduct.RetailPrice;
                buyer.Inventory += NumOrdered;
                Console.WriteLine("============================");
                foreach (Business b in SellerList)
                {
                    b.Inventory -= NumPerSeller;
                    b.Funds += NumPerSeller * order.OrderProduct.RetailPrice;
                    Console.WriteLine(b.Name + " " + b.Inventory + ", " + b.Funds);
                }
                Console.WriteLine("============================");
                Console.WriteLine("Market total: " + Market.ComStock);
            } else if(first.Type.Equals("I", StringComparison.CurrentCultureIgnoreCase))
            {
                if(Market.IndStock < NumOrdered)
                {
                    foreach (Business b in SellerList)
                    {
                        b.FillInventory();
                    }
                }
                Market.IndStock -= NumOrdered;
                buyer.Funds -= NumOrdered * order.OrderProduct.WholesalePrice;
                buyer.Inventory += NumOrdered;
                Console.WriteLine("============================");
                foreach (Business b in SellerList)
                {
                    b.Inventory -= NumPerSeller;
                    b.Funds += NumPerSeller * order.OrderProduct.WholesalePrice;
                    Console.WriteLine(b.Name + " " + b.Inventory + ", " + b.Funds);
                }
                Console.WriteLine("============================");
                Console.WriteLine("Market total: " + Market.IndStock);
            }
            

            
            

























            //Console.WriteLine("Businesses available " + SellerList.Count);
            //int transfer;
            //for (int i = 0; i < SellerList.Count && quantityOrdered > 0; ++i)
            //{
            //    Business seller = SellerList[i];
            //    //If business carries product being ordered

            //    int amountAvailable = seller.Inventory.Amount;
            //    //if seller does not have enough to complete order.
            //    if (amountAvailable < quantityOrdered)
            //    {
            //        transfer = (int)(order.OrderProduct.RetailPrice * amountAvailable);
            //        seller.Inventory.Amount -= amountAvailable;
            //        //seller needs to order more
            //        seller.FillInventory();
            //        quantityOrdered -= amountAvailable;
            //        //b.Inventory[order.OrderProduct] -= amountAvailable;
            //        seller.Funds += transfer;
            //        buyer.Funds -= transfer;
            //    }
            //    //if seller can complete order
            //    else if (amountAvailable >= quantityOrdered)
            //    {
            //        transfer = (int)(order.OrderProduct.RetailPrice * quantityOrdered);
            //        seller.Inventory.Amount -= quantityOrdered;
            //        quantityOrdered = 0;
            //        //b.Inventory[order.OrderProduct] -= amountAvailable;
            //        seller.Funds += transfer;
            //        buyer.Funds -= transfer;


            //    }

            //}

        }
    }
}
