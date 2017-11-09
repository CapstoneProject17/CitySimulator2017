using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Econ
{
    class Order
    {
        public Product OrderProduct { get; }
        public int Amount { get; }
        public ICustomer Sender { get; }

        public Order (Product prod, int Amount, ICustomer Sender)
        {
            this.OrderProduct = prod;
            this.Amount = Amount;
            this.Sender = Sender;
        }

    }
}
