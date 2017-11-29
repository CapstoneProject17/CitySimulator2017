namespace DBInterface.Econ
{
    /// <summary>
    /// Order class. Used in Market to handle transactions
    /// </summary>
    /// <para>Written by Chandu Dissanayake </para>
    public class Order
    {
        //Product being ordered
        public Product OrderProduct { get; }
        //Amount of product ordered
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
