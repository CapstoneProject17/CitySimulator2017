namespace DBInterface.Econ
{
    /// <summary>
    /// Objects that can make orders implement this interface
    /// <para>Written by Connor Goudie 2017-11-08</para>
    /// Last modified by Justin McLennan 2017-11-30</para>
    /// </summary>
    public interface ICustomer
    {
        //amount of money a customer has
        double Funds { get; set; }
        //amount of product
        double Inventory { get; set; }
    }
}
