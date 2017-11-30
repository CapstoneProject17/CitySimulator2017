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

        //minum number of a single product allowed to be in Inventory until more are ordered
        public const int MINIMUM_VALUE = 200;

        //average salary of workers
        public int TotalPayout { get; set; }

        //list of all current employees
        public List<Person> workers { get; set; }

        //products currently being sold
        public double Inventory { get; set; }

        //current bank account balance
        public double Funds { get; set; }

        /// <summary>
        /// Default constructor for a Business
        /// <para>Written by Chandu Dissanayake 2017-11-08</para>
        /// </summary>
        public Business() : base()
        {
            workers = new List<Person>();
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
            workers = new List<Person>();

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
            int TotalPayout = 0;
            foreach (Person w in workers)
            {
                w.Funds += w.Salary;
                Funds -= w.Salary;
                TotalPayout += w.Salary;

            }
            Console.WriteLine(Name + ": Monthly Payout :" + TotalPayout);
        }
    }
}
