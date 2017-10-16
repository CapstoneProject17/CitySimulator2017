using MongoDB.Bson;

namespace DataAccessLayer {
    class Roads {
        public ObjectId Id { get; set; }
        public int RoadCoordinates { get; set; }
    }
}
