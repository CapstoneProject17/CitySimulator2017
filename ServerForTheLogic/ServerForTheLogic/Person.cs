using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.Canada;
using Bogus;
using ConsoleDump;
using ServerForTheLogic.Utilities;
using ServerForTheLogic.Infrastructure;
using static Bogus.DataSets.Name;
using Newtonsoft.Json;
using ServerForTheLogic.Econ;

namespace ServerForTheLogic
{
    [JsonObject(MemberSerialization.OptIn)]
    class Person : ICustomer
    {

        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;
        public int Funds { get; set; }
        [JsonProperty]
        /// <summary>
        /// ID for database
        /// </summary>
        public Guid Id { get; private set; }

        [JsonProperty]
        /// <summary>
        /// Person's first name
        /// </summary>
        public string FName { get; private set; }

        [JsonProperty]
        /// <summary>
        /// Person's last name
        /// </summary>
        public string LName { get; private set; }

        [JsonProperty]
        /// <summary>
        /// Funds earned every 4 weeks of simulation time
        /// </summary>
        public int MonthlyIncome { get; set; }

        [JsonProperty]
        /// <summary>
        /// where this person works 
        /// </summary>
        /// <param name="works"></param>
        /// <returns></returns>
        public Building Workplace { get; set; }

        [JsonProperty]
        /// <summary>
        /// Where this person lives
        /// </summary>
        public Building Home { get; set; }
        
        [JsonProperty]
        /// <summary>
        /// Number of days remaining until person dies
        /// </summary>
        public int DaysLeft;

        [JsonProperty]
        /// <summary>
        /// Current Age in years
        /// </summary>
        public int Age;

        /// <summary>
        /// Current Age in years
        /// </summary>
        private bool isDead;

        /// <summary>
        /// Time to go to work
        /// </summary>
        public int TimeToWork { get; }

        /// <summary>
        /// Time to go Home
        /// </summary>
        public int TimeToHome { get; }

        /// <summary>
        /// Rate at which Food is Consumed daily
        /// </summary>
        public int DCR;

        /// <summary>
        /// Big Expense cost changed when bought by the person
        /// </summary>
        public int BExp;

        /// <summary>
        /// Yearly Salary
        /// </summary>
        public int Salary;

        /// <summary>
        /// Number of Products the person owns
        /// </summary>
        public int NumProducts;

        public Person(string fName, string lName,City c)
        {
            FName = fName;
            LName = lName;
            isDead = false;
            Id = Guid.NewGuid();
            setDeathAge();
            //consumption created
            DailyConsumption();
            NumProducts = 0;//ToBuy();
            //Big expense created
            BigExpense();
            //Calculate Salary
            
            Funds = new Randomizer().Number(1000, 10000);
            TimeToWork = new Randomizer().Number(0, 23);
            TimeToHome = (TimeToWork + 8) % 24;
        }

        /// <summary>
        /// Changes Daily Consumption rate
        /// </summary>
        public void DailyConsumption() {
            DCR = new Randomizer().Number(3, 4);
        }

        /// <summary>
        /// Changes Big Expense value
        /// </summary>
        public void BigExpense() {
            BExp = new Randomizer().Number(5000, 13000);
        }

        /// <summary>
        /// Amount to buy
        /// </summary>
        public int ToBuy() {
            return new Randomizer().Number(42, 56);
        }

        public void incomeGenerated(Business b) {
            if (b.Type.Equals("C", StringComparison.CurrentCultureIgnoreCase))
                MonthlyIncome = new Randomizer().Number(3000, 5000);
            else 
                MonthlyIncome = new Randomizer().Number(4000, 6000);
            Salary = MonthlyIncome * 12;
        }
        /// <summary>
        /// Randomly generates an age this person will die (in days) based on
        /// guassian distribution.
        /// </summary>
        public void setDeathAge()
        {
            //if we want to add additional randomness to death age
            //int months = random.Next(1, 13);
            //int days = random.Next(1, 30);

            //generates a random number of years left to live
            double u1 = 1.0 - new Randomizer().Double(0, 1);//uniform(0,1] random doubles
            double u2 = 1.0 - new Randomizer().Double(0, 1);
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = MEAN_DEATH_AGE + STANDARD_DEVIATION_DEATH * randStdNormal; //random normal(mean,stdDev^2)

            //converts years left to days
            DaysLeft = (int)randNormal * 365;
        }

        /// <summary>
        /// Prints out a person's attributes in a neatly formatted manner
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return FName + " " + LName + " " + "Monthly income: " +
                MonthlyIncome + " Current money: " + Funds + " Unique ID: " + Id;
        }

        /// <summary>
        /// Decrements the Days left for a person
        /// </summary>
        public bool AgeDeathTick()
        {
            DaysLeft--;
            if (DaysLeft <= 0)
                isDead = true;
            return isDead;
        }

        /// <summary>
        /// Buys a random thing from products (singular right now)
        /// </summary>
        /// Written by Chandu Dissanayake
        public void BuyThings()
        {
            int rand = new Randomizer().Number(0, Market.Products.Count - 1);
            if (Funds >= Market.Products[rand].RetailPrice)
            {
                Order order = new Order(Market.Products[rand], ToBuy(), this);
                // Funds -= (int)order.OrderProduct.RetailPrice * order.Amount;
                Market.ProcessOrder(order, Market.CommercialBusinesses);
                Console.WriteLine("Bought " + order.OrderProduct.ProductName + " " + order.Amount);
                NumProducts += order.Amount;
            }
        }
        /// <summary>
        /// Sets Age of Person
        /// </summary>
        public void SetAge() {
                Age++;
        }

        public void ConsumeProd() {
            if (NumProducts > DCR)
                NumProducts -= DCR;
            else {
                BuyThings();
                NumProducts -= DCR;
            }

            
        }

        
    }
}
