using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using DBInterface.Infrastructure;
using DBInterface;
using ServerForTheLogic.Json.LiteObjects;

namespace ServerForTheLogic.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    class ClientPacket
    {
        const string PATH = @"..\..\fullPacket.json";

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

        static ClientPacket()
        {
            // This text is added only once to the file.
            if (!File.Exists(PATH))
            {
                File.WriteAllText(PATH, "");
            }
        }

        public string ConvertPartialPacket()
        {
            NewRoads = city.NewRoads;
            NewBuildings = city.NewBuildings;
            NetHours = city.clock.NetHours;
            PeopleMoving = city.PartialUpdateList[(int)NetHours % 24];
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(PATH))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                sw.Close();
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

       
            string JsonString =  JsonConvert.SerializeObject(this, Formatting.Indented);
            city.SendtoDB();

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

            using (StreamWriter sw = new StreamWriter(PATH))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                sw.Close();
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

            city.NewBuildings = new List<Building>();
            city.NewRoads = new List<Point>();

            return JsonString;
        }
    }
}
