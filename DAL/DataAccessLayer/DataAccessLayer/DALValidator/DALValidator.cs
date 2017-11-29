using ServerForTheLogic;
using ServerForTheLogic.ClientObject;
using ServerForTheLogic.ClientObject.Building;
using ServerForTheLogic.DALValidator;
using ServerForTheLogic.DALValidator.ValidationHelper;
using ServerForTheLogic.DALValidator.ValidationHelper.GridObjectsValidation.BuildingValidation;
using ServerForTheLogic.Econ;
using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// <para>
    /// MongoDAL Validator
    /// Team: DB
    /// Validator used for the MongoDAL class when insert, update is requested.
    /// Has validator for citizen.
    /// Author: Sean 
    /// Date: 2017-10-31
    /// Based on: http://cacodaemon.de/index.php?id=42
    ///           https://docs.mongodb.com/manual/core/document-validation/
    ///           http://www.c-sharpcorner.com/UploadFile/87b416/validating-user-input-with-regular-expressions/
    /// </para>
    /// <para>
    /// Updated: Bill</para>
    /// Date: 2017-11-01
    /// <para>seperated each validation method.</para>
    /// <para>
    /// <para>Updated: Bill</para>  
    /// <para>Date: 2017-11-11</para>
    ///     updated validation parameters to work for new classes
    ///     Citizens class changed to Person class
    ///     Building class changed to Industrial, Commercial and Residential classes
    ///     added new classes: Road, Product, Clock, SaveState
    /// </para>    
    /// </summary>
    class DALValidator
    {
        /// <summary>
        /// Person Validator
        /// 
        /// Check whether all the fields in person is valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-11
        /// <para>For more information see <see cref="PersonValidator"/></para>
        /// </summary>
        /// <param name="person">The person object being validated</param>
        /// <returns>If all validations pass it returns true, otherwise flase</returns>
        public static Boolean DALPersonValidator(Person person)
        {
            if(PersonValidator.isValidPersonFirstName(person.FName) &&
               PersonValidator.isValidPersonLastName(person.LName) &&
               PersonValidator.isValidPersonMonthlyIncome(person.MonthlyIncome) &&
               PersonValidator.isValidPersonAccountBalance(person.Funds) &&
               PersonValidator.isValidPersonWorkplaceID(person.Workplace.id.ToString()) &&
               PersonValidator.isValidPersonWorkplaceX(person.Workplace.Point.x) &&
               PersonValidator.isValidPersonWorkplaceY(person.Workplace.Point.z) &&
               PersonValidator.isValidPersonHomeID(person.Home.id.ToString()) &&
               PersonValidator.isValidPersonHomeX(person.Home.Point.x) &&
               PersonValidator.isValidPersonHomeY(person.Home.Point.z) &&
               PersonValidator.isValidPersonDaysLeft(person.DaysLeft) &&
               PersonValidator.isValidPersonAge(person.Age) &&
               PersonValidator.isValidPersonStartShift(person.TimeToWork) &&
               PersonValidator.isValidPersonEndShift(person.TimeToHome))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Grid Object Validator
        /// 
        /// Checks whether all the fields in a GridObject are valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-12
        /// <para>For more information see <see cref="GridObjectValidator"/></para>
        /// </summary>
        /// <param name="location">The GridObject being validated</param>
        /// <returns>Returns true if everything passes</returns>
        public static Boolean DALGridObjectValidator(Location location)
        {
            return GridObjectValidator.isValidXCoordinate(location.Point.x) &&
                GridObjectValidator.isValidYCoordinate(location.Point.z);
        }

        /// <summary>
        /// Building Validator
        /// 
        /// Checks whether all the fields in a Building is valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-12
        /// <para>For more information see <see cref="BuildingValidator"/></para>
        /// </summary>
        /// <param name="building">The building object being validated</param>
        /// <returns></returns>
        public static Boolean DALBuildingValidator(Building building)
        {
            //connor
            return DALGridObjectValidator(building) &&
                BuildingValidator.isValidBuildingCapacity(building.Capacity) &&
                BuildingValidator.isValidBuildingRating(building.Rating);
        }

        /// <summary>
        /// Industrial Building Validator
        /// 
        /// Check whether all the fields in industrialBuilding is valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-12
        /// <para>For more information see <see cref="IndustrialValidator"/></para>
        /// </summary>
        /// <param name="industrialBuilding">The Industrial building being validated</param>
        /// <returns>Returns true if the Industrial building validates</returns>
        public static Boolean DALIndustrialBuildingValidator(Industrial industrialBuilding)
        {
            //connor will fix
            return DALBuildingValidator(industrialBuilding) &&
                IndustrialValidator.isValidIndustrialInventoryCount(industrialBuilding.inventory.Count) &&
                //TODO: loop these
                IndustrialValidator.isValidIndustrialProductionCost(industrialBuilding) &&
                IndustrialValidator.isValidIndustrialWholesalePrice(industrialBuilding.WholesalePrice);
        }

        /// <summary>
        /// <para>
        /// Commercial Building Validator.
        /// Checks whether all the fields in commercialBuilding is valid
        /// </para>
        /// <para>
        /// Author: Bill
        /// <para>Date: 2017-11-12 </para>
        /// </para>
        /// <para>See <see cref="CommercialValidator"/> for more information on Commercial building validation requirments</para>
        /// </summary>
        /// <param name="commercialBuilding">The Commercial building being validated</param>
        /// <returns>Returns true if the building validates</returns>
        public static Boolean DALCommercialBuildingValidator(Commercial commercialBuilding)
        {
            foreach (KeyValuePair<Product, int> p in commercialBuilding.inventory)
            {
                if (!CommercialValidator.isValidCommercialInventoryCount(commercialBuilding.inventory[p.Key]) &&
                    CommercialValidator.isValidCommercialRetailPrice(commercialBuilding.inventory[p.Key]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Residential Building Validator.
        ///  
        /// Check whether all the fields in residentialBuilding is valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-12
        /// <para>For more information see <see cref="ResidentialValidator"/></para>
        /// </summary>
        /// <param name="residentialBuilding">The Residential building being validated</param>
        /// <returns>Returns true if the building validates</returns>
        public static Boolean DALResidentialBuildingValidator(Residential residentialBuilding)
        {
            return DALBuildingValidator(residentialBuilding);
        }

        /// <summary>
        /// Road Validator.
        /// 
        /// Check whether all the fields in road is valid
        /// 
        /// <para>Author: Bill </para>
        /// Date: 2017-11-12 
        /// <para>For more information see <see cref="GridObjectValidator"/></para>
        /// <param name="road">The road being validated</param>
        /// <returns>Returns true if the road validates</returns>
        public static Boolean DALRoadValidator(Road road)
        {
            return DALGridObjectValidator(road);
        }

        /// <summary>
        /// Clock Validator.
        /// 
        /// Check whether all the fields in clock is valid
        /// 
        /// <para>Author: Bill</para>
        /// Date: 2017-11-12 
        /// <para>For more information see <see cref="ClockValidator"/></para>
        /// </summary>
        /// <param name="clock">The clock object being validated</param>
        /// <returns>Returns true if the clock validates</returns>
        public static Boolean DALClockValidator(Clock clock)
        {
            return ClockValidator.isValidClockNetMinutes(clock.NetMinutes) &&
                ClockValidator.isValidClockNetHours(clock.NetHours);
        }

        /// <summary>
        /// Product Validator
        /// 
        /// Check whether all the fields in product is valid
        /// 
        /// <para> Author: Bill </para>
        /// Date: 2017-11-12 
        /// <para>For more information see <see cref="ProductValidator"/></para>
        /// </summary>
        /// <param name="product">The product being validated</param>
        /// <returns>Returns true if the product validates</returns>
        public static Boolean DALProductValidator(Product product)
        {
            //TODO: fix this
            return ProductValidator.isValidProductGlobalCount((int)product.ManufacturingPrice) &&
                ProductValidator.isValidProductName(product.ProductName);
        }

        /// <summary>
        /// SaveState Validator
        /// 
        /// Check whether all the fields in saveState is valid
        /// 
        /// Author: Bill
        /// Date: 2017-11-12 
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public static Boolean DALSaveStateValidator(SaveState saveState)
        {
            return SaveStateValidator.isValidBackupState(saveState.BackupState) &&
                SaveStateValidator.isValidSaveStateId(saveState.Id);
        }
    }
}
