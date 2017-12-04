namespace DALInterface 
{
    public struct Point
    {
        /// <summary>
        /// Basic position struct for buildings and roads
        /// </summary>
        /// <para>Written by Connor Goudie </para>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public Point(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public int x { get; }
        public int z { get; }

        public override string ToString()
        {
            return "X: " + x + " Z: " + z;
        }
    }
}
