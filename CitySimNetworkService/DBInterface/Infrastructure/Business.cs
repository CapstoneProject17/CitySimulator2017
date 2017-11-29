using DBInterface.Econ;
using System;
using System.Collections.Generic;

namespace DBInterface.Infrastructure
{
    /// <summary>
    /// Business is an extension of the building object, which contains a dictionary
    /// of products, has methods to pay employees, and maintains a list of workers.
    /// Implements ICustomer, for transactions
    /// <para>Written by Chandu Dissanayake, Justin McLennan, Connor Goudie 2017-11-08</para>
    /// <para>Last modified by Connor Goudie 2017-11-14</para>
    /// </summary>
    public class Business : Building, ICustomer
    {
        //base amount of money all businesses start with
        public static int FIXED_FUNDS = 20000;

        //minum number of a single product allowed to be in inventory until more are ordered
        public const int MINIMUM_VALUE = 5;

        //Multiplier that increases or decreases worker wage
        public double WageMultiplier { get; set; }

        //average salary of workers
        public int AvgWorkerSalary { get; set; }

        //list of all current employees
        public List<Person> workers { get; set; }

        //products currently being sold
        public Dictionary<Product, int> inventory { get; set; }

        //current bank account balance
        public int Funds { get; set; }

        /// <summary>
        /// Default constructor for a Business
        /// <para>Written by Chandu Dissanayake 2017-11-08</para>
        /// </summary>
        public Business() : base()
        {
            Funds = FIXED_FUNDS;
            WageMultiplier = 0.5;
            workers = new List<Person>();
            inventory = new Dictionary<Product, int>();
        }

        /// <summary>
        /// Overloaded construct for business
        /// <para>Written by Chandu Dissanayake 2017-11-08</para>
        /// <para>Last modified by Chandu Dissanayake 2017-11-14</para>
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="capacity"></param>
        /// <param name="isTall"></param>
        public Business(string Name, int capacity, Boolean isTall) : base(Name, capacity, isTall)
        {
            Funds = FIXED_FUNDS;
            WageMultiplier = 0.5;
            workers = new List<Person>();
            inventory = new Dictionary<Product, int>();

            //ONLY RUN ONCE NOW BECAUSE ONLY ONE PRODUCT
            AddProductToInventory();
        }

        /// <summary>
        /// Adds a product that exists in the world market, but is not currently in 
        /// this businesses' inventory.
        /// <para>Written by Chandu Dissanayake, Justin McLennan, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Andrew Busto 2017-11-14</para>
        /// </summary>
        public virtual void AddProductToInventory()
        {
            //Console.WriteLine(Market.ProductsInDemand + "  " + Market.ProductsInDemand.Count);
            if (Market.ProductsInDemand != null && Market.ProductsInDemand.Count > 0)
            {
                foreach (Product p in Market.ProductsInDemand)
                {
                    if (!inventory.ContainsKey(p))
                    {
                        inventory.Add(p, MINIMUM_VALUE);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Unimplemented
        /// </summary>
        public virtual void FillInventory()
        {

        }

        /// <summary>
        /// Currently unimplemted; pay employees, if the business does not have enough
        /// funds to pay every employee, lay off a certain number
        /// <para>Written by Chandu Dissanayake, Andrew Busto 2017-11-08</para>
        /// <para>Last modified by Chandu Dissanayake 2017-11-13</para>
        /// </summary>
        public void PayEmployees()
        {
            if (AvgWorkerSalary == 0)
            {
                AvgWorkerSalary = (int)((WageMultiplier) * Funds) / (workers.Count);
                //MAYBE SET MONTHLY INCOME FOR EMPLOYEES
            }
            while (AvgWorkerSalary * (workers.Count) > Funds)
            {
                //FIRE PPL
            }
            foreach (Person w in workers)
            {
                w.Funds += AvgWorkerSalary;
                Funds -= AvgWorkerSalary;
            }
        }
    }
}
