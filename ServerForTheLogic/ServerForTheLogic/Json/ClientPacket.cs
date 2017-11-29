using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using ServerForTheLogic.Json.LiteObjects;

namespace ServerForTheLogic.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    class ClientPacket
    {
        //[JsonProperty]
        //public List<Person> PeopleSent { get; set; }

        //[JsonProperty]
        //public List<Location> Locations { get; set; }

        //[JsonProperty]
        //public Point GridSize { get; set; }

        [JsonProperty]
        public int GridLength { get; set; }

        [JsonProperty]
        public int GridWidth { get; set; }

        [JsonProperty]
        public UInt32 NetHours { get; set; }
        //[JsonProperty]
        //public List<Person> NewPeople { get; set; }

        [JsonProperty]
        public List<Point> NewRoads { get; set; }
        [JsonProperty]
        public List<Building> NewBuildings { get; set; }

        public City city { get; set; }

        [JsonProperty]
        public List<PersonTravel> PeopleMoving { get; set; }

        //public Dictionary<Guid, Point> PeopleMoving { get; set; }

        //[JsonProperty]
        public string[,] QuickMap { get; set; }


        public ClientPacket(City city)
        {
            this.city = city;

            GridLength = City.CITY_LENGTH;
            GridWidth = City.CITY_WIDTH;

            NewRoads = city.NewRoads;
            NewBuildings = city.NewBuildings;
            PeopleMoving = new List<PersonTravel>();
        }


        public string ConvertPartialPacket()
        {
            NewRoads = city.NewRoads;
            NewBuildings = city.NewBuildings;
            NetHours = city.clock.NetHours;
            PeopleMoving = city.PartialUpdateList[(int)NetHours % 24];
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter("partialPacket.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                sw.Close();
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

       
            string JsonString =  JsonConvert.SerializeObject(this, Formatting.Indented);
            //Console.WriteLine("CONVERT PACKET");

            //Console.WriteLine(JsonString);
            city.NewBuildings = new List<Building>();
            city.NewRoads = new List<Point>();

            return JsonString;
        }

        public string ConvertFullPacket()
        {
            NewRoads = city.AllRoads;
            NewBuildings = city.AllBuildings;
            NetHours = city.clock.NetHours;
            PeopleMoving = city.PartialUpdateList[(int)NetHours % 24];
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter("fullPacket.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                sw.Close();
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }


            string JsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            //Console.WriteLine("CONVERT PACKET");

            //Console.WriteLine(JsonString);
            city.NewBuildings = new List<Building>();
            city.NewRoads = new List<Point>();

            return JsonString;
        }


        //public void fillQuickMap(City city)
        //{

        //    for (int i = 0; i < City.CITY_WIDTH; ++i)
        //    {
        //        for (int j = 0; j < City.CITY_LENGTH; ++j)
        //        {
        //            if (city.Map[i, j] != null)
        //            {

        //                QuickMap[i, j] = city.Map[i, j].Type;

        //            }
        //            else
        //            {
        //                QuickMap[i, j] = ".";

        //            }
        //        }
        //    }
        //}
    }
}
