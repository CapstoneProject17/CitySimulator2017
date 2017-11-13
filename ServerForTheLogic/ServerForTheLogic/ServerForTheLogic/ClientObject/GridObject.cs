using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerForTheLogic.ClientObject
{
    /// <summary>
    /// Grid Object
    /// Team: DB
    /// GridObject contains all the general fields of grid objects
    /// 
    /// Author: Bill
    /// Based on: N/A
    /// Update: N/A
    /// </summary>
    class GridObject
    {
        /// <summary>
        /// ID for the grid object
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// XPoint must be a valid X-Coordinate
        /// </summary>
        public int XPoint { get; set; }

        /// <summary>
        /// YPoint must be a valid Y-Coordinate
        /// </summary>
        public int YPoint { get; set; }
    }
}
