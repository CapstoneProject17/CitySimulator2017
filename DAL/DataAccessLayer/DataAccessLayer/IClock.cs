using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    interface IClock
    {
        UInt32 NetMinutes { get; set; }
        UInt32 NetHours{ get; set; }
    }
}
