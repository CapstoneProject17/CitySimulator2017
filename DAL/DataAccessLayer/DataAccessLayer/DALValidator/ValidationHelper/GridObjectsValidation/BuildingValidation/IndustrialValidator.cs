using System;

namespace ServerForTheLogic.DALValidator
{
    /// <summary>
    /// Industrial Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the validation rules for Industrial Building.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class IndustrialValidator : BuildingValidator
    {
        /// <summary>
        /// Industrial Building Inventory Count Validator
        /// 
        /// Validation Rule:
        ///     1. industrialInventoryCount must be a positive int
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="industrialInventoryCount"></param>
        /// <returns></returns>
        public static Boolean isValidIndustrialInventoryCount(int industrialInventoryCount)
        {
            if (industrialInventoryCount < 0)
            {
                Console.WriteLine("Invalid industrial building inventory count: " + industrialInventoryCount);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Industrial Building Production Cost Validator
        /// 
        /// Validation Rule:
        ///     1. industrialProductionCost must be a positive int
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="industrialProductionCost"></param>
        /// <returns></returns>
        public static Boolean isValidIndustrialProductionCost(int industrialProductionCost)
        {
            if (industrialProductionCost < 0)
            {
                Console.WriteLine("Invalid industrial building production cost: " + industrialProductionCost);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Industrial Building Wholesale Price Validator
        /// 
        /// Validation Rule:
        ///     1. industrialWholesalePrice must be a positive int
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="industrialWholesalePrice"></param>
        /// <returns></returns>
        public static Boolean isValidIndustrialWholesalePrice(int industrialWholesalePrice)
        {
            if (industrialWholesalePrice < 0)
            {
                Console.WriteLine("Invalid industrial building wholesale price: " + industrialWholesalePrice);
                return false;
            }
            return true;
        }
    }
}
