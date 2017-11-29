namespace DBInterface.Infrastructure
{
    /// <summary>
    /// Roads are used as a means to get a person from 1 point to another, and serve
    /// no other purpose.
    /// <para>Written by Connor Goudie 2017-10-02</para>
    /// </summary>
    public class Road : Location
    {
        /// <summary>
        /// Default constructor for road
        /// <para>Written by Connor Goudie 2017-10-02</para>
        /// </summary>
        public Road() : base()
        {
            this.Type = "R";
        }

        /// <summary>
        /// Overloaded constructor for road
        /// <para>Written by Connor Goudie 2017-10-02</para>
        /// </summary>
        public Road(string Name) : base(Name)
        {
            this.Type = "R";
        }
    }
}
