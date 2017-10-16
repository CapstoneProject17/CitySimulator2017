using MongoDB.Bson;

namespace DataAccessLayer {
    class Buildings {
        public ObjectId Id { get; set; }
        public string BuildingName { get; set; }
        public string BuildingType { get; set; }
        public string BuildingCompany { get; set; }
        public int BuildingLocation { get; set; }
        public int BuildingMoney { get; set; }
        public int BuildingLevel { get; set; }
    }
}
