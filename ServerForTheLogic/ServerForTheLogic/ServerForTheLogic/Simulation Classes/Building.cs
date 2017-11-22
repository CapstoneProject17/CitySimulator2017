using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ServerForTheLogic.Simulation_Classes;

namespace ServerForTheLogic.Classes
{
    public class Building
    {
        //ratings for all buildings
        public static int POOR = 0;
        public static int MIDDLE_CLASS = 1;
        public static int RICH = 2;

        // rating of the building, 1-3
        private int rating;
        //need to add data to DB in constructor then pull ID from DB
        private string ID;
        // x position
        public int X { get; set; }
        //z position
        public int Z { get; set; }
        //amount of money being earned per day
        private int dailyIncome;
        //amount of money building has currently
        private int currentMoney;
        private Human[] employees;
        /// <summary>
        /// Constructor for building object
        /// </summary>
        public Building()
        {

        }

        /// <summary>
        /// End of every work day update amount of money owned 
        /// </summary>
        private void addDailyIncome()
        {

        }

        private void payEmployees()
        {
            foreach (Human h in employees)
            {
                h.pay();
            }
        }
    }
}
