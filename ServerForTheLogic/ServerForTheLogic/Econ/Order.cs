using ServerForTheLogic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    /// <summary>
    /// Order class. Used in Market to handle transactions
    /// </summary>
    /// <para>Written by Chandu Dissanayake </para>
    class Order
    {
        //Product being ordered
        public Product OrderProduct { get; }
        //Amount of product ordered
        public int Amount { get; }
        
        public ICustomer Buyer { get; }

        public Order (Product prod, int Amount, ICustomer Sender)
        {
            this.OrderProduct = prod;
            this.Amount = Amount;
            this.Buyer = Sender;
        }
    }
}
