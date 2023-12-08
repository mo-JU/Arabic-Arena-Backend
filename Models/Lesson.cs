using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Lesson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string TitleArabic { get; set; }
    public string TitleEnglish { get; set; }
    public string Level { get; set; }
    public string Type { get; set; }
    public bool Video { get; set; }
    public string VideoLink { get; set; }
    public string VideoText { get; set; }
    public string Text { get; set; }
    public bool HasTable { get; set; }

    [BsonElement("Table")]
    public List<TableItem> Table { get; set; }

    public bool HasExercises { get; set; }

    [BsonElement("Exercises")]
    public List<Exercise> Exercises { get; set; }

    public string ImageLink { get; set; }
}

public class TableItem
{
    public string ArabicWord { get; set; }
    public string Transcription { get; set; }
}

public class Exercise
{
    public string QuestionArabic { get; set; }
    public string QuestionEnglish { get; set; }
    public string QuestionType { get; set; }
    public string AudioWord { get; set; }
    public List<string> Options { get; set; }
    public List<string> CorrectAnswer { get; set; }
}