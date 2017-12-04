namespace DBInterface 
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
            this.X = x;
            this.Z = z;
        }

        public int X { get; }
        public int Z { get; }

        public override string ToString()
        {
            return "X: " + X + " Z: " + Z;
        }
    }
}
