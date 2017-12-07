using DataAccessLayer;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.DALValidator.ValidationHelper
{
    /// <summary>
    /// SaveState Validator
    /// Team: DB
    /// Helper class for DALValidator. 
    /// This class contains all the validation rules for SaveState.
    /// 
    /// Author: Bill 
    /// Date: 2017-11-12
    /// Based on: N/A   
    /// Update: N/A
    /// </summary>
    class SaveStateValidator
    {
        /// <summary>
        /// SaveState Id Validator
        /// 
        /// Validation Rules:
        ///     N/A
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="saveStateId"></param>
        /// <returns></returns>
        public static Boolean isValidSaveStateId(ObjectId saveStateId)
        {
            //TODO: We need more info on how to validate SavaState.
            return true;
        }

        /// <summary>
        /// SaveState Backup State Validator
        /// 
        /// Validation Rules:
        ///     N/A
        ///     
        /// Author: Bill
        /// Date: 2017-11-12
        /// </summary>
        /// <param name="saveStateId"></param>
        /// <returns></returns>
        public static Boolean isValidBackupState(List<SaveState> saveStateBackupState)
        {
            //TODO: We need more info on how to validate SavaState.
            return true;
        }
    }
}
