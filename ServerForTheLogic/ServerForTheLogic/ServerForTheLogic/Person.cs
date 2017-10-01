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

namespace ServerForTheLogic
{
    public class Person
    {
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;


        //ID for database 
        private Guid id;

        public string FName { get; private set; }
        public string LName { get; private set; }
        
       
        //location for human's workplace 
        //private Point jobPoint; 
        //location for human's home 
        //private Point homePoint; 
        //amount of income earned per month 
        public int MonthlyIncome { get; set; }
        public Gender Gender { get; set; }

        //current amount of money in bank account 
        private int money; 
        //where this person works 
        private Building workplace; 
        //where this person lives 
        private Building home; 
        //male or female, made it a string if we want to add more genders. 
        //private string gender; 
        private bool isDead; 
        //number of days remaining until human dies 
        private int DaysLeft;

        public Person()
        {
            id = new Guid();
        }



        public void setDeathAge()
        {
            Random random = new Random();
            //int months = random.Next(1, 13);
            //int days = random.Next(1, 30);
            
            Random rand = new Random(); //reuse this if you are generating many
           
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = MEAN_DEATH_AGE + STANDARD_DEVIATION_DEATH * randStdNormal; //random normal(mean,stdDev^2)
            DaysLeft = (int) randNormal * 365;
            
        }

        public override String ToString()
        {
            return FName + " " + LName + " " + Gender.ToString() + "\n";
        }
        

    }
    


}
