using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Person class mapped to the Person collection of the database and the "actors" in the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// </summary>
    class Person
    {
        public string FirstName { get; set; }
        public string LastNamee { get; set; }
        public int MonthlyIncome { get; set; }
        public int AccountBalance { get; set; }
        public string WorkplaceID { get; set; } //type?
        public string WorkplaceX { get; set; }//split up??
        public string WorkplaceY { get; set; }
        public string HomeID { get; set; } //type?
        public string HomeX { get; set; }
        public string HomeY { get; set; }
        public int DaysLeft { get; set; }
        public int StartShift { get; set; }
        public int EndShift { get; set; }
    }
}
