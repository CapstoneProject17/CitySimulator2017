using System;
using System.Collections.Generic;
using System.Text;

namespace DBInterface
{
    /// <summary>
    /// Interface used to pass clock object to the DAL
    /// </summary>
    public interface IClock
    {
        //total simulated minutes the clock has been running
        UInt32 NetMinutes { get; set; }
        //total simulated hours the clock has been running
        UInt32 NetHours { get;set; }
        //total simulated days the clock has been running
        UInt32 NetDays { get; set; }
        //total simulated years the clock has been running
        UInt32 NetYears { get; set; }
    }
}
