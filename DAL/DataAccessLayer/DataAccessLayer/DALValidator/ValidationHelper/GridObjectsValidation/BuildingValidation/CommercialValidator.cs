using System;

namespace ServerForTheLogic.DALValidator.ValidationHelper.GridObjectsValidation.BuildingValidation
{
    /// <summary>
    /// Commercial Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the validation rules for Commercial Buildings.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class CommercialValidator : BuildingValidator
    {
        /// <summary>
        /// Commercial Building Retail Price Validator
        /// 
        /// Validation Rule:
        ///     1. commercialRetailPrice must be a valid integer
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="commercialRetailPrice"></param>
        /// <returns>Returns true if the retail price validates</returns>
        public static Boolean isValidCommercialRetailPrice(int commercialRetailPrice)
        {
            if (commercialRetailPrice == int.MaxValue || commercialRetailPrice == int.MinValue)
            {
                Console.WriteLine("Invalid commercial building retail price: " + commercialRetailPrice);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Commercial Building Inventory Count Validator
        /// 
        /// Validation Rule:
        ///     1. commercialInventoryCount must be a valid integer
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="industrialInventoryCount"></param>
        /// <returns>True if the inventory count validates</returns>
        public static Boolean isValidCommercialInventoryCount(int commercialInventoryCount)
        {
            if (commercialInventoryCount == int.MaxValue || commercialInventoryCount == int.MinValue)
            {
                Console.WriteLine("Invalid commercial building inventory count: " + commercialInventoryCount);
                return false;
            }
            return true;
        }
    }
}
