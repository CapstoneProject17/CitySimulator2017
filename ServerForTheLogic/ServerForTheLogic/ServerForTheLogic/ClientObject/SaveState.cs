using MongoDB.Bson;
using System.Collections.Generic;

namespace DataAccessLayer {
    /// <summary>
    /// SaveState Class
    /// Team: DB
    /// SaveState backs up the current state of the simulation.
    /// Author: Bill 
    /// Date: 2017-11-12 
    /// </summary>
    class SaveState {
        public ObjectId Id { get; set; }
        public List<SaveState> BackupState { get; set; }
    }
}
