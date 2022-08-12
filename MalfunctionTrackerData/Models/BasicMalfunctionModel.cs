namespace MalfunctionTrackerData.Models
{
    public class BasicMalfunctionModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Malfunction { get; set; }

        public BasicMalfunctionModel()
        {

        }

        public BasicMalfunctionModel(MalfunctionModel malfunction)
        {
            Id = malfunction.Id;
            Malfunction = malfunction.Malfunction;
        }
    }
}
