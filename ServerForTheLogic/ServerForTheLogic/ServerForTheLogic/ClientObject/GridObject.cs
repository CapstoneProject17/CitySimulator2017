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
    public class GridObject
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="xPoint"></param>
        /// <param name="yPoint"></param>
        public GridObject(Guid guid, int xPoint, int yPoint)
        {
            Guid = guid;
            XPoint = xPoint;
            YPoint = yPoint;
        }

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
