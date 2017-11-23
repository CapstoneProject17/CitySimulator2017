using MongoDB.Bson;
using System.Collections.Generic;

namespace DataAccessLayer {
    class SaveState {
        public ObjectId Id { get; set; }
        public List<SaveState> BackupState { get; set; }
    }
}
