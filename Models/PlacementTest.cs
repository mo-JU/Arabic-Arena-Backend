using MongoDB.Bson.Serialization.Attributes;

namespace Arabic_Arena.Models
{
    public class PlacementTest
    {
        public string id { get; set; }
        public int time { get; set; }
        public int advanced { get; set; }
        public int intermediate { get; set; }

        [BsonElement("questions")]
        public List<Question> questions { get; set; }

    }
}
