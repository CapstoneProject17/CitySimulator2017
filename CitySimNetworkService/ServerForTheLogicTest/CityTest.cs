using CitySimNetworkService;
using ServerForTheLogic;
using System;
using Xunit;
using DBInterface.Infrastructure;
using DBInterface;

namespace ServerForTheLogicTest
{
    /// <summary>
    /// Unit tests for City.
    /// <para>Written by Andrew Busto 2017-12-09</para>
    /// </summary>
    public class CityTest
    {
        City city = new City(new SimulationStateQueue(), new SimulationStateQueue());

        /// <summary>
        /// Checks to make sure a location can be retrieved
        /// given valid x y coordinates.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(City.CITY_WIDTH-1, 0)]
        [InlineData(0, City.CITY_LENGTH-1)]
        [InlineData(City.CITY_WIDTH - 1, City.CITY_LENGTH - 1)]
        public void GetLocationAt_ValidPosition_NoException(int x, int y)
        {
            city.GetLocationAt(x, y);
        }

        /// <summary>
        /// Checks to make sure an exception is thrown
        /// when GetLocationAt is passed invalid coordinates.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(-1, -1)]
        [InlineData(City.CITY_WIDTH, 0)]
        [InlineData(0, City.CITY_LENGTH)]
        [InlineData(City.CITY_WIDTH, City.CITY_LENGTH)]
        public void GetLocationAt_InvalidPosition_Exception(int x, int y)
        {
            Action act = () => city.GetLocationAt(x, y);
            Assert.ThrowsAny<IndexOutOfRangeException>(act);
        }

        /// <summary>
        /// Checks to make sure a location can be retrieved
        /// given a valid point.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(City.CITY_WIDTH - 1, 0)]
        [InlineData(0, City.CITY_LENGTH - 1)]
        [InlineData(City.CITY_WIDTH - 1, City.CITY_LENGTH - 1)]
        public void GetLocationAtPoint_ValidPosition_NoException(int x, int y)
        {
            Point p = new Point(x, y);

            city.GetLocationAt(p);
        }

        /// <summary>
        /// Checks to make sure an exception is thrown
        /// when GetLocationAt is passed an invalid point.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(-1, -1)]
        [InlineData(City.CITY_WIDTH, 0)]
        [InlineData(0, City.CITY_LENGTH)]
        [InlineData(City.CITY_WIDTH, City.CITY_LENGTH)]
        public void GetLocationAtPoint_InvalidPosition_Exception(int x, int y)
        {
            Point p = new Point(x, y);

            Action act = () => city.GetLocationAt(p);
            Assert.ThrowsAny<IndexOutOfRangeException>(act);
        }

        /// <summary>
        /// Checks to make sure no exception is thrown when
        /// ExpandCity is called with a valid blocktype for
        /// expansion.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(BlockType.Residential)]
        [InlineData(BlockType.Commercial)]
        [InlineData(BlockType.Industrial)]
        public void ExpandCity_ValidBlockTypes_NoException(BlockType t)
        {
            city.ExpandCity(t);
        }

        /// <summary>
        /// Checks to make sure an exception is thrown when
        /// ExpandCity is called with the blocktype Empty.
        /// <para>Written by Andrew Busto 2017-12-09</para>
        /// </summary>
        [Theory]
        [InlineData(BlockType.Empty)]
        public void ExpandCity_InvalidBlockTypes_Exception(BlockType t)
        {
            Action act = () => city.ExpandCity(t);
            Assert.ThrowsAny<Exception>(act);
        }
    }
}
