using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.ClientObject
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Person class mapped to the Person collection of the database and the "actors" in the simulation.
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// 
    /// Update:
    /// 2017-11-11 Bill
    ///     - changed WorkplaceX, WorkplaceY, HomeX and HomeY's parameter type to int (was string)
    ///     - added new field "age"
    /// 
    /// 2017-11-12 Bill
    ///     - added summary for fields
    /// </summary>
    class Person
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="monthlyIncome"></param>
        /// <param name="accountBalance"></param>
        /// <param name="workPlaceID"></param>
        /// <param name="workplaceX"></param>
        /// <param name="workplaceY"></param>
        /// <param name="homeID"></param>
        /// <param name="homeX"></param>
        /// <param name="homeY"></param>
        /// <param name="daysLeft"></param>
        /// <param name="age"></param>
        /// <param name="startShift"></param>
        /// <param name="endShift"></param>
        public Person(string firstName, string lastName, int monthlyIncome, int accountBalance, string workPlaceID, int workplaceX, int workplaceY, string homeID, int homeX, int homeY, int daysLeft, int age, int startShift, int endShift)
        {
            FirstName = firstName;
            LastName = lastName;
            MonthlyIncome = monthlyIncome;
            AccountBalance = accountBalance;
            WorkplaceID = workPlaceID;
            WorkplaceX = workplaceX;
            WorkplaceY = workplaceY;
            HomeID = homeID;
            HomeX = homeX;
            HomeY = homeY;
            DaysLeft = daysLeft;
            Age = age;
            StartShift = startShift;
            EndShift = endShift;
        }
        /// <summary>
        /// 1. FirstName can not be null
        /// 2. FirstName can not be empty
        /// 3. FirstName can not be longer than 30 letters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 1. LastName can not be null
        /// 2. LastName can not be empty
        /// 3. LastName can not be longer than 30 letters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// MonthlyIncome can not be negative
        /// </summary>
        public int MonthlyIncome { get; set; }

        /// <summary>
        /// AccountBalance can not be negative
        /// </summary>
        public int AccountBalance { get; set; }

        /// <summary>
        /// WorkplaceID must be a valid Guid
        /// </summary>
        public string WorkplaceID { get; set; }

        /// <summary>
        /// WorkplaceX must be a valid X-Coordinate
        /// </summary>
        public int WorkplaceX { get; set; }

        /// <summary>
        /// WorkplaceY must be a valid Y-Coordinate
        /// </summary>
        public int WorkplaceY { get; set; }

        /// <summary>
        /// HomeID must be a valid Guid
        /// </summary>
        public string HomeID { get; set; }

        /// <summary>
        /// HomeX must be a valid X-Coordinate
        /// </summary>
        public int HomeX { get; set; }

        /// <summary>
        /// HomeY must be a valid Y-Coordinate
        /// </summary>
        public int HomeY { get; set; }

        /// <summary>
        /// DaysLeft must be an int between 0 - 125
        /// </summary>
        public int DaysLeft { get; set; }

        /// <summary>
        /// Age can not be negative
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// start shift time must be an int between 0 - 23
        /// </summary>
        public int StartShift { get; set; }

        /// <summary>
        /// end shift time must be an int between 0 - 23
        /// </summary>
        public int EndShift { get; set; }
    }
}
