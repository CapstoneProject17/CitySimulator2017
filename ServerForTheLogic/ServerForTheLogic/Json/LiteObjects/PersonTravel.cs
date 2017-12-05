using DBInterface;
using DBInterface.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace ServerForTheLogic.Json.LiteObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PersonTravel
    {
        [JsonProperty]
        public Guid Id { get;  set; }

        [JsonProperty]
        public Point Origin { get; set; }

        [JsonProperty]
        public Point Destination { get; set; }

        public PersonTravel(Guid Id, Point Origin, Point Destination, City city)
        {
            this.Id = Id;
            this.Origin = FindClosestRoad(Origin, city);
            this.Destination = FindClosestRoad(Destination,city);
            //Console.WriteLine("Origin before = " + Origin + " Origin after = " + this.Origin);
            //Console.WriteLine("Dest before = " + Destination + " Dest after = " + this.Destination);

        }

        /// <summary>
        /// 
        /// Finds closest road
        /// </summary>
        /// <param name="start"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public Point FindClosestRoad(Point start, City city)
        {
            Type type = typeof(DBInterface.Infrastructure.Road) ;
            if (city.Map[start.X - 1, start.Z]!=null && (city.Map[start.X - 1, start.Z].GetType() == type))
            {
                return new Point(start.X - 1, start.Z);
            }
            else if (city.Map[start.X + 1, start.Z]!=null && city.Map[start.X + 1, start.Z].GetType() == type)
            {
                return new Point(start.X + 1, start.Z);
            }
            else if (city.Map[start.X, start.Z - 1]!=null && city.Map[start.X, start.Z - 1].GetType() == type)
            {
                return new Point(start.X, start.Z - 1);
            }
            else if (city.Map[start.X, start.Z + 1]!=null && city.Map[start.X, start.Z + 1].GetType() == type)
            {
                return new Point(start.X, start.Z + 1);
            }
            else
            {
                return start;
            }
        }
    }
}
