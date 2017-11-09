using ServerForTheLogic.Econ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class Business : Building, ICustomer
    {
        public static int FIXED_FUNDS = 20000;
        public const int MINIMUM_VALUE = 5;

        public double WageMultiplier { get; set; }
        public int AvgWorkerSalary { get; set; }
        public List<Person> workers { get; set; }

        /// <summary>
        /// Products being sold.
        /// </summary>
        public Dictionary<Product, int> inventory { get; set; }
        public int Funds { get; set; }

        public Business() : base()
        {
            Funds = FIXED_FUNDS;
            WageMultiplier = 0.5;
            workers = new List<Person>();
            inventory = new Dictionary<Product, int>();
        }

        public Business(string Name, int capacity) : base(Name, capacity)
        {
            Funds = FIXED_FUNDS;
            WageMultiplier = 0.5;
            workers = new List<Person>();
            inventory = new Dictionary<Product, int>();

            //ONLY RUN ONCE NOW BECAUSE ONLY ONE PRODUCT
            AddProductToInventory();
        }

        public virtual void AddProductToInventory()
        {
            //Console.WriteLine(Market.ProductsInDemand + "  " + Market.ProductsInDemand.Count);
            if (Market.ProductsInDemand != null && Market.ProductsInDemand.Count > 0)
            {
                foreach (Product p in Market.ProductsInDemand)
                {
                    if (!inventory.ContainsKey(p))
                    {
                        inventory.Add(p, 0);
                        return;
                    }
                }
            }
        }

        public virtual void FillInventory()
        {

        }

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
            }
        }
    }
}
