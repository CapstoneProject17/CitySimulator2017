using System;

namespace ServerForTheLogic.DALValidator.ValidationHelper
{
    /// Clock Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the validation rules for Clock.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    class ClockValidator
    {
        /// <summary>
        /// Clock Net Minutes Validator
        /// 
        /// Validation Rule:
        ///     1. clockNetMinutes must be an unsigned int between 0 - 59
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="clockNetMinutes"></param>
        /// <returns></returns>
        public static Boolean isValidClockNetMinutes(UInt32 clockNetMinutes)
        {
            if (clockNetMinutes < 0 || clockNetMinutes > 59)
            {
                Console.WriteLine("Invalid clock net minutes: " + clockNetMinutes);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clock Net Hours Validator
        /// 
        /// Validation Rule:
        ///     1. clockNetHours must be an unsigned int between 0 - 23
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="clockNetHours"></param>
        /// <returns></returns>
        public static Boolean isValidClockNetHours(UInt32 clockNetHours)
        {
            if (clockNetHours < 0 || clockNetHours > 23)
            {
                Console.WriteLine("Invalid clock net hours: " + clockNetHours);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clock Net Days Validator
        /// 
        /// Validation Rule:
        ///     1. clockNetDays must be an unsigned int between 0 - 365
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="clockNetHours"></param>
        /// <returns></returns>
        public static bool isValidClockNetDays(UInt32 clockNetDays)
        {
            if (clockNetDays < 0 || clockNetDays > 365)
            {
                Console.WriteLine("Invalid clock net days: " + clockNetDays);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clock Net Years Validator
        /// 
        /// Validation Rule:
        ///     1. clockNetYears must be an unsigned int 
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="clockNetYears"></param>
        /// <returns></returns>
        public static bool isValidClockNetYears(UInt32 clockNetYears)
        {
            if (clockNetYears < 0 || clockNetYears == UInt32.MaxValue)
            {
                Console.WriteLine("Invalid clock net years: " + clockNetYears);
                return false;
            }
            return true;
        }
    }
}
