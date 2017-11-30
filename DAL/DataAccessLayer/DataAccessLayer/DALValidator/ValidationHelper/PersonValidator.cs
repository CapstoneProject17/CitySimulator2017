using System;

namespace ServerForTheLogic.DALValidator
{
    /// <summary>
    /// Person Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the validation rules for Person class.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-11
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class PersonValidator
    {
        /// <summary>
        /// Person First Name Validator
        /// 
        /// Validation Rules:
        ///     1. first name can not be null
        ///     2. first name can not be empty
        ///     3. first name can not be longer than 30 letters
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="personFirstName"></param>
        /// <returns></returns>
        public static Boolean isValidPersonFirstName(string personFirstName)
        {
            if (personFirstName == null || personFirstName.CompareTo(string.Empty) == 0 || personFirstName.Length > 30)
            {
                Console.WriteLine("Invalid person first name: " + personFirstName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person Last Name Validator
        /// 
        /// Validation Rules:
        ///     1. last name can not be null
        ///     2. last name can not be empty
        ///     3. last name can not be longer than 30 letters
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="personLastName"></param>
        /// <returns></returns>
        public static Boolean isValidPersonLastName(string personLastName)
        {
            if (personLastName == null || personLastName.CompareTo(string.Empty) == 0 || personLastName.Length > 30)
            {
                Console.WriteLine("Invalid person last name: " + personLastName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person Monthly Income Validator
        /// 
        /// Validation Rules:
        ///     1. person monthly income can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="monthlyIncome"></param>
        /// <returns></returns>
        public static Boolean isValidPersonMonthlyIncome(int monthlyIncome)
        {
            if (monthlyIncome < 0)
            {
                Console.WriteLine("Invalid person monthly income: " + monthlyIncome);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Person Account Balance Validator
        /// 
        /// Validation Rules:
        ///     1. account balance can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="accountBalance"></param>
        /// <returns></returns>
        public static Boolean isValidPersonAccountBalance(int accountBalance)
        {
            if (accountBalance < 0)
            {
                Console.WriteLine("Invalid person account balance: " + accountBalance);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person Work Place ID Validator
        /// 
        /// Validation Rules:
        ///     1. workplace ID must be a valid Guid
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="workplaceID"></param>
        /// <returns></returns>
        public static Boolean isValidPersonWorkplaceID(string workplaceID)
        {
            return GridObjectValidator.isValidGuid(workplaceID);
        }

        /// <summary>
        /// Person Work Place X Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. workplace X coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="workplaceX"></param>
        /// <returns></returns>
        public static Boolean isValidPersonWorkplaceX(int workplaceX)
        {
            return GridObjectValidator.isValidXCoordinate(workplaceX);
        }

        /// <summary>
        /// Person Work Place Y Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. workplace Y coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="workplaceY"></param>
        /// <returns></returns>
        public static Boolean isValidPersonWorkplaceY(int workplaceY)
        {
            return GridObjectValidator.isValidYCoordinate(workplaceY);
        }

        /// <summary>
        /// Person Home ID Validator
        /// 
        /// Validation Rules:
        ///     1. home ID must be a valid Guid
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="homeID"></param>
        /// <returns></returns>
        public static Boolean isValidPersonHomeID(string homeID)
        {
            return GridObjectValidator.isValidGuid(homeID);
        }

        /// <summary>
        /// Person Home X Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. home X coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="homeX"></param>
        /// <returns></returns>
        public static Boolean isValidPersonHomeX(int homeX)
        {
            return GridObjectValidator.isValidXCoordinate(homeX);
        }

        /// <summary>
        /// Person Home Y Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. home Y coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="homeY"></param>
        /// <returns></returns>
        public static Boolean isValidPersonHomeY(int homeY)
        {
            return GridObjectValidator.isValidYCoordinate(homeY);
        }

        /// <summary>
        /// Person Days Left Validator
        /// 
        /// Validation Rules:
        ///     1. days left must be an int between 0 - 125
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="personDaysLeft"></param>
        /// <returns></returns>
        public static Boolean isValidPersonDaysLeft(int personDaysLeft)
        {
            if (personDaysLeft < 0 || personDaysLeft > 50000)
            {
                Console.WriteLine("Invalid person days left: " + personDaysLeft);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person Age Validator
        /// 
        /// Validation Rules:
        ///     1. age can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static Boolean isValidPersonAge(int age)
        {
            if (age < 0)
            {
                Console.WriteLine("Invalid person age: " + age);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person Start Shift (time to go to work) Validator
        /// 
        /// Validation Rules:
        ///     1. start shift time must be an int between 0 - 23
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="personStartShift"></param>
        /// <returns></returns>
        public static Boolean isValidPersonStartShift(int personStartShift)
        {
            if (personStartShift < 0 || personStartShift > 23)
            {
                Console.WriteLine("Invalid start shift time: " + personStartShift);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Person End Shift (time to go home) Validator
        /// 
        /// Validation Rules:
        ///     1. end shift time must be an int between 0 - 23
        ///     
        /// Author: Bill
        /// Date: 2017-11-11
        /// </summary>
        /// <param name="personEndShift"></param>
        /// <returns></returns>
        public static Boolean isValidPersonEndShift(int personEndShift)
        {
            if (personEndShift < 0 || personEndShift > 23)
            {
                Console.WriteLine("Invalid end shift time: " + personEndShift);
                return false;
            }
            return true;
        }
    }
}
