using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerForTheLogic.DALValidator
{
    /// <summary>
    /// Grid Object Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the general validations that can be reused by  GridObject classes.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class GridObjectValidator
    {
        /// <summary>
        /// Regex for checking valid Guid
        /// 
        /// Author: Bill
        /// Date: 2017-11-12
        /// Based on: https://stackoverflow.com/questions/2370689/how-to-validate-guid-in-net
        /// </summary>
        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        /// <summary>
        /// Guid Validator
        /// Validation rule is based on isGuid Regex
        /// 
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="guidString"></param>
        /// <returns></returns>
        public static Boolean isValidGuid(string guidString)
        {
            return isGuid.IsMatch(guidString);
        }

        /// <summary>
        /// Grid X Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. X coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="xCord"></param>
        /// <returns></returns>
        public static Boolean isValidXCoordinate(int xCord)
        {
            if (xCord < 0)
            {
                Console.WriteLine("Invalid X coordinate: " + xCord);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Grid Y Coordinate Validator
        /// 
        /// Validation Rules:
        ///     1. Y coordinate can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="yCord"></param>
        /// <returns></returns>
        public static Boolean isValidYCoordinate(int yCord)
        {
            if (yCord < 0)
            {
                Console.WriteLine("Invalid Y coordinate: " + yCord);
                return false;
            }
            return true;
        }


    }
}
