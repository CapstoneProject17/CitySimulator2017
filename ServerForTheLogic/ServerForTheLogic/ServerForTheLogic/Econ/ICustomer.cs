using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    /// <summary>
    /// Objects that can make orders implement this interface
    /// </summary>
    interface ICustomer
    {
        int Funds { get; set; }

    }
}
