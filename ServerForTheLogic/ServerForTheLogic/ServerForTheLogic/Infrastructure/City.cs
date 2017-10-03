using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    class City
    {
        /// <summary>
        /// Max width of the city grid
        /// </summary>
        public const int CITY_WIDTH = 100;
        /// <summary>
        /// Max length of the city grid
        /// </summary>
        public const int CITY_LENGTH = 100;

        /// <summary>
        /// List of all homes in the city
        /// </summary>
        public List<Residential> homes { get; set; }
        /// <summary>
        /// List of all places of work in the city
        /// </summary>
        public List<Building> workplaces { get; set; }
        /// <summary>
        /// List of all inhabitants of the city
        /// </summary>
        public List<Person> AllPeople { get; set; }
        /// <summary>
        /// The grid that all buildings/roads/people exist in
        /// </summary>
        public Location[,] map { get; set; }
        /// <summary>
        /// 2D array of city blocks, this is for easier city expansion
        /// NOTE: not currently implemented
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public Block[,] blockMap { get; set; }

        /// <summary>
        /// Constructor for a new city, creates the 100x100 grid of cells,
        /// and the grid of city blocks
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public City()
        {
            map = new Location[CITY_WIDTH, CITY_LENGTH];
            blockMap = new Block[CITY_WIDTH / 3, CITY_LENGTH / 6];
            AllPeople = new List<Person>();
            homes = new List<Residential>();
            workplaces = new List<Building>();

           // TO DO: code to create initial state, or load from DB

           // blocks.Add(new Block(new Point(CITY_WIDTH / 2, CITY_LENGTH / 2)));
            
        }

        /// <summary>
        /// Returns the location object at the specified point
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Location GetLocationAt(Point p)
        { 
            return map[p.x, p.z];
        }
        /// <summary>
        /// Returns the location object at the specified x z coordinate 
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Location GetLocationAt(int x, int z)
        {
            return map[x, z];
        }
    }
}
