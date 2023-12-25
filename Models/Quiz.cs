using MongoDB.Bson.Serialization.Attributes;

namespace Arabic_Arena.Models
{
    public class Quiz
    {
        public string id { get; set; }

        public string titleArabic { get; set; }
        public string titleEnglish { get; set; }
        public string level { get; set; }
        public string type { get; set; }
        public int time { get; set; }

        [BsonElement("questions")]
        public List<Question> questions { get; set; }

    }

    public class Question
    {
        public string questionArabic { get; set; }
        public string questionEnglish { get; set; }
        public string questionType { get; set; }
        public string audioWord { get; set; }
        public List<string> options { get; set; }
        public List<string> correctAnswer { get; set; }
    }
}
