using MongoDB.Bson;

namespace DataAccessLayer {
    class Citizens {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int HomeAddress { get; set; }
        public int WorkAddress { get; set; }
        public int Age { get; set; }
        public int DaysLeftToLive { get; set; } // Prophetic
    }
}
