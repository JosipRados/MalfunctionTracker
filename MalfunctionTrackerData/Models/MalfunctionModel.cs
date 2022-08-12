using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalfunctionTrackerData.Models
{
    public class MalfunctionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Malfunction { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public CategoryModel Category { get; set; }
        public BasicUserModel User { get; set; }
        public HashSet<string> UserVotes { get; set; } = new();
        public StatusModel MalfunctionStatus { get; set; }
        public string MaintenanceNotes { get; set; }
        public bool Acknowledged { get; set; } = false;
        public bool Archived { get; set; } = false;
        public bool Rejected { get; set; } = false;
    }
}
