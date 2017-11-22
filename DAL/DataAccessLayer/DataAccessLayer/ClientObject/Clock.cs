namespace ServerForTheLogic.ClientObject
{
    /// <summary>
    /// MongoDAL
    /// Team: DB
    /// Clock used for timing and synchronization
    /// Author: Stephanie 
    /// Date: 2017 11 08
    /// 
    /// Update:
    /// 2017-11-12 Bill
    ///     - added new field 'NetYears'
    ///     - added summary for fields
    /// </summary>
    public class Clock
    {
        public Clock(int netMinutes, int netHours, int netDays, int netYears)
        {
            NetMinutes = netMinutes;
            NetHours = netHours;
            NetDays = netDays;
            NetYears = netYears;
        }

        /// <summary>
        /// BSON objectId stored in the database.
        /// </summary>
        public MongoDB.Bson.ObjectId _id { get; set; }

        /// <summary>
        /// NetMinutes must be an unsigned int between 0 - 59
        /// </summary>
        public int NetMinutes { get; set; }

        /// <summary>
        /// NetHours must be an unsigned int between 0 - 23
        /// </summary>
        public int NetHours { get; set; }

        /// <summary>
        /// NetDays must be an unsigned int between 0 - 365
        /// </summary>
        public int NetDays { get; set; }

        /// <summary>
        /// NetYears must be an unsigned int
        /// </summary>
        public int NetYears { get; set; }
    }
}
