 using System;

namespace ServerForTheLogic.DALValidator
{
    /// <summary>
    /// Building Validations
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the general validations functions that can be reused by building classes.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class BuildingValidator : GridObjectValidator
    {
        /// <summary>
        /// Building Rating Validator
        /// 
        /// Validation Rule:
        ///     1. rating must be an int between 1 - 3
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="buildintRating"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingRating(int buildintRating)
        {
            if(buildintRating < 0 || buildintRating > 2)
            {
                Console.WriteLine("Invalid building rating: " + buildintRating);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Building Capacity Validator
        /// 
        /// Validation Rule:
        ///     1. capacity must be an int between 0 - 100
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="buildingCapacity"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingCapacity(int buildingCapacity)
        {
            if (buildingCapacity < 0 || buildingCapacity > 100)
            {
                Console.WriteLine("Invalid building capacity: " + buildingCapacity);
                return false;
            }
            return true;
        }

    }
}
