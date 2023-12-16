using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Lesson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }

    public string titleArabic { get; set; }
    public string titleEnglish { get; set; }
    public string level { get; set; }
    public string type { get; set; }
    public bool video { get; set; }
    public string videoLink { get; set; }
    public string videoText { get; set; }
    public string text { get; set; }
    public bool hasTable { get; set; }

    [BsonElement("table")]
    public List<TableItem> table { get; set; }

    public bool hasExercises { get; set; }

    [BsonElement("exercises")]
    public List<Exercise> exercises { get; set; }

    public string imageLink { get; set; }
}

public class TableItem
{
    public string arabicWord { get; set; }
    public string transcription { get; set; }
}

public class Exercise
{
    public string questionArabic { get; set; }
    public string questionEnglish { get; set; }
    public string questionType { get; set; }
    public string audioWord { get; set; }
    public List<string> options { get; set; }
    public List<string> correctAnswer { get; set; }
}