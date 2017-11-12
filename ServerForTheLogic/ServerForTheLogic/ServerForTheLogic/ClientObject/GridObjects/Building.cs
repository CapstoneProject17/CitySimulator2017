using ServerForTheLogic.ClientObject.GridObjects;
using System;

namespace ServerForTheLogic.ClientObject.Building
{
    /// <summary>
    /// Building Class
    /// Team: DB
    /// Building class that contain general fields for all building types.
    /// Author: Bill 
    /// Date: 2017-11-12 
    /// </summary>
    class Building : GridObject
    {    
        /// <summary>
        /// Rating is an int between 1 - 3
        /// </summary>            
        public int Rating { get; set; }

        /// <summary>
        /// is this a tall building? This is used for rendering graphics model
        /// </summary>
        public Boolean IsTall { get; set; }

        /// <summary>
        /// max number of person allowed in the building
        /// </summary>
        public int Capacity { get; set; }
    }
}
