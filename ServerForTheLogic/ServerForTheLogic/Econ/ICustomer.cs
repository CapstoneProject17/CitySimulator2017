using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    /// <summary>
    /// Objects that can make orders implement this interface
    /// <para>Written by Connor Goudie 2017-11-08</para>
    /// </summary>
    interface ICustomer
    {
        //amount of money a customer has
        double Funds { get; set; }
        //amount of product
        double Inventory { get; set; }
    }
}
