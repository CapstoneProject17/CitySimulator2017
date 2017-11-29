namespace DBInterface.Econ
{
    /// <summary>
    /// Objects that can make orders implement this interface
    /// <para>Written by Connor Goudie 2017-11-08</para>
    /// </summary>
    public interface ICustomer
    {
        //amount of money a customer has
        int Funds { get; set; }
    }
}
