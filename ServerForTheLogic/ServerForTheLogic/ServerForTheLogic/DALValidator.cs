using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// MongoDAL Validator
    /// Team: DB
    /// Validator used for the MongoDAL class when insert, update is requested.
    /// Has validator for citizen.
    /// Author: Sean 
    /// Date: 2017-10-31
    /// Based on: http://cacodaemon.de/index.php?id=42
    ///           https://docs.mongodb.com/manual/core/document-validation/
    ///           http://www.c-sharpcorner.com/UploadFile/87b416/validating-user-input-with-regular-expressions/
    ///           
    /// Update:
    /// 2017-11-01 Bill
    ///     seperated each validation method.
    /// </summary>
    class DALValidator
    {
        public static Boolean CitizenValidator(Citizens citizen)
        {
            if(isValidCitizenName(citizen.Name) &&
               isValidCitizenSalary(citizen.Salary) &&
               isValidCitizenHomeAddress(citizen.HomeAddress) &&
               isValidCitizenWorkAddress(citizen.WorkAddress) &&
               isValidCitizenAge(citizen.Age) &&
               isValidCitizenDaysLeftToLive(citizen.DaysLeftToLive))
            {
                return true;
            }
            return false;
            // Salary, HomeAddress, WorkAddress, Age and DaysLeftToLive cannot be a negative number
           
            if(citizen.HomeAddress < 0 || citizen.WorkAddress < 0)
            {
                Console.Write("Must be greater than 0");
                return false;
            }
            if(citizen.Age <= 0)
            {
                Console.Write("Age cannot be less than 0");
                return false;
            }
            if(citizen.DaysLeftToLive < 0)
            {
                Console.Write("Days cannot be less than 0");
                return false;
            }
        }

        /// <summary>
        /// Citizen Name Validator
        /// 
        /// Validation Rules:
        ///     1. name can not be empty
        ///     2. no empty space at begining and end
        ///     3. maximum 3 names (first, middle and last) seperated by 2 spaces 
        ///     4. each name has to start with uppercase and minimum 2 chars each
        /// 
        /// Example:
        ///     "Aa Bb Cc" -- vaild
        ///     " Aa Bb Cc" -- invalid
        ///     "Aaaa Bb" -- valid
        ///     "aa Bb Cc" -- invalid
        ///     
        /// Author: Bill, Sean
        /// Date: 2017-11-01
        /// </summary>
        /// <param name="citizenName"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenName(string citizenName)
        {
            if (citizenName == "" ||
                !Regex.Match(citizenName, @"^[A-Z][a-zA-Z]{2,}(?:[A-Z][a-zA-Z]*){0,2}$").Success)
            {
                Console.WriteLine("Invalid citizen name: " + citizenName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Citizen Salary Validator
        /// 
        /// Validation Rules:
        ///     1. citizen salary can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-01
        /// </summary>
        /// <param name="citizenSalary"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenSalary(int citizenSalary)
        {
            if (citizenSalary < 0)
            {
                Console.WriteLine("Invalid citizen salary: " + citizenSalary);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Citizen Home Address Validator
        /// 
        /// Validation Rules:
        ///     1. citizen home address can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: get more info about this field.. shouldn't the address be an int array? (2D grid)
        /// </summary>
        /// <param name="citizenHomeAddress"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenHomeAddress(int citizenHomeAddress)
        {
            if (citizenHomeAddress < 0)
            {
                Console.WriteLine("Invalid citizen home address: " + citizenHomeAddress);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Citizen Work Address Validator
        /// 
        /// Validation Rules:
        ///     1. citizen work address can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: get more info about this field.. shouldn't the address be an int array? (2D grid)
        /// </summary>
        /// <param name="citizenWorkAddress"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenWorkAddress(int citizenWorkAddress)
        {
            if (citizenWorkAddress < 0)
            {
                Console.WriteLine("Invalid citizen work address: " + citizenWorkAddress);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Citizen Age Validator
        /// 
        /// Validation Rules:
        ///     1. citizen age can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: get more info about this field. is there a maximum age?
        /// </summary>
        /// <param name="citizenAge"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenAge(int citizenAge)
        {
            if (citizenAge < 0)
            {
                Console.WriteLine("Invalid citizen age: " + citizenAge);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Citizen Days Left to Live Validator
        /// 
        /// Validation Rules:
        ///     1. citizen days left to live can not be negative
        ///     
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// </summary>
        /// <param name="citizenDaysLeftToLive"></param>
        /// <returns></returns>
        public static Boolean isValidCitizenDaysLeftToLive(int citizenDaysLeftToLive)
        {
            if (citizenDaysLeftToLive < 0)
            {
                Console.WriteLine("Invalid citizen days left to live: " + citizenDaysLeftToLive);
                return false;
            }
            return true;
        }

        /// <summary>
        /// General building validator
        /// 
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: we might need seperate validator for different types of buildings as the client team is deciding 
        ///       whether each building has different fields
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns >
        public static Boolean BuildingValidator(Buildings building)
        {
            if(isValidBuildingName(building.BuildingName) &&
               isValidBuildingType(building.BuildingType) &&
               isValidBuildingCompany(building.BuildingCompany) &&
               isValidBuildingLocation(building.BuildingLocation) &&
               isValidBuildingMoney(building.BuildingMoney) &&
               isValidBuildingLevel(building.BuildingLevel))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Building Name Validator
        /// 
        /// Validation rule: 
        ///     1. building name can not be empty
        ///     2. building name can not be longer than 50 chars
        /// 
        /// Author: Bill
        /// Date: 2017-11-01
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingName(string buildingName)
        {
            if (buildingName.CompareTo(string.Empty) == 0 || buildingName.Length > 50)
            {
                Console.WriteLine("Invalid building name: " + buildingName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Building Type Validator
        /// 
        /// Validation rule: 
        ///     1. building type can not be empty
        ///     2. building type must be one of "residential", "commercial", or "industrial"
        ///     3. no case sensitivity
        ///     4. extra space will be trimmed before validation
        /// 
        /// Author: Bill
        /// Date: 2017-11-01
        /// </summary>
        /// <param name="buildingType"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingType(string buildingType)
        {
            string mBuildingType = buildingType.ToLower().Trim();
            if (mBuildingType.CompareTo(string.Empty) == 0 ||
                mBuildingType.CompareTo("residential") != 0 ||
                mBuildingType.CompareTo("commercial") != 0 ||
                mBuildingType.CompareTo("industrial") != 0)
            {
                Console.WriteLine("Invalid building type: " + buildingType);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Building Company Validator
        /// 
        /// Validation rule: 
        ///     1. building company can not be empty
        ///     2. building company can not be longer than 50 chars
        /// 
        /// Author: Bill
        /// Date: 2017-11-01
        /// <param name="buildingCompany"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingCompany(string buildingCompany)
        {
            if (buildingCompany.CompareTo(string.Empty) == 0 || buildingCompany.Length > 50)
            {
                Console.WriteLine("Invalid building company: " + buildingCompany);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Building Location Validator
        /// 
        /// Validation rule:
        ///     1. building location must be greater or equals to 0
        ///
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: get more validation rules from client team. shouldn't location be an int[]?
        /// </summary>
        /// <param name="buildingLocation"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingLocation(int buildingLocation)
        {
            if(buildingLocation < 0)
            {
                Console.WriteLine("Invalid building location: " + buildingLocation);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Building Money Validator
        /// 
        /// Validation rule:
        ///     1. building money must be greater or equals to 0
        ///
        /// Author: Bill
        /// Date: 2017-11-01
        /// </summary>
        /// <param name="buildingMoney"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingMoney(int buildingMoney)
        {
            if (buildingMoney < 0)
            {
                Console.WriteLine("Invalid building money: " + buildingMoney);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Building Level Validator
        /// 
        /// Validation rule:
        ///     1. building level must be greater or equals to 0
        ///
        /// Author: Bill
        /// Date: 2017-11-01
        /// 
        /// TODO: get more info on building level restriction. is it max 3 levels?
        /// </summary>
        /// <param name="buildingLevel"></param>
        /// <returns></returns>
        public static Boolean isValidBuildingLevel(int buildingLevel)
        {
            if (buildingLevel < 0)
            {
                Console.WriteLine("Invalid building level: " + buildingLevel);
                return false;
            }
            return true;
        }
    }
}
