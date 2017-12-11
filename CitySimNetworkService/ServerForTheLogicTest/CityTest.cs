using CitySimNetworkService;
using ServerForTheLogic;
using System;
using Xunit;
using DBInterface.Infrastructure;
using DBInterface;

namespace ServerForTheLogicTest
{
    public class CityTest
    {
        City city = new City(new SimulationStateQueue(), new SimulationStateQueue());

        [Theory]
        [InlineData(0, 0)]
        [InlineData(City.CITY_WIDTH-1, 0)]
        [InlineData(0, City.CITY_LENGTH-1)]
        [InlineData(City.CITY_WIDTH - 1, City.CITY_LENGTH - 1)]
        public void GetLocationAt_ValidPosition_NoException(int x, int y)
        {
            city.GetLocationAt(x, y);
        }

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

        [Theory]
        [InlineData(BlockType.Residential)]
        [InlineData(BlockType.Commercial)]
        [InlineData(BlockType.Industrial)]
        public void ExpandCity_ValidBlockTypes_NoException(BlockType t)
        {
            city.ExpandCity(t);
        }

        [Theory]
        [InlineData(BlockType.Empty)]
        public void ExpandCity_InvalidBlockTypes_Exception(BlockType t)
        {
            Action act = () => city.ExpandCity(t);
            Assert.ThrowsAny<Exception>(act);
        }
    }
}
