using Arabic_Arena.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Arabic_Arena.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly IMongoCollection<Lesson> _lessonsCollection;

        public LessonsController(MongoDbContext context)
        {
            _lessonsCollection = context.Lessons; // Replace with your actual collection name
        }

        // Get all lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get()
        {
              var lessons = await _lessonsCollection.Find(_ => true).ToListAsync();
             return Ok(lessons);
           /* var lesson = new Lesson
            {
                titleArabic = "فوائد الشاي الأخضر",
                titleEnglish = "Benefits of Green Tea",
                level = "Beginner",
                type = "Reading",
                video = true,
                videoLink = "https://edpuzzle.com/media/6553a11ba164024027006fdc",
                videoText = "nigga", // Add full HTML content as needed
    text = "hi", // Add full HTML content as needed
    hasTable = true,
    table = new List<TableItem>
    {
        new TableItem { arabicWord = "الشاي", transcription = "Al-Shai" },
        new TableItem { arabicWord = "سرطان", transcription = "Saratan" }
    },
    hasExercises = true,
    exercises = new List<Exercise>
    {
        new Exercise
        {
            questionArabic = "كم حرف باللغة العربية؟",
            questionEnglish = "How many letters are in the Arabic language?",
            questionType = "multipleChoice",
            audioWord = "",
            options = new List<string> { "28", "27", "26", "25" },
            correctAnswer = new List<string> { "28" }
        },
        new Exercise
        {
            questionArabic = "ما هي أحرف هذه الكلمة؟",
            questionEnglish = "Which of these letters compose this word?",
            questionType = "allThatApply",
            audioWord = "مَلِكْ",
            options = new List<string> { "الميم", "الكاف", "الألف", "اللام" },
            correctAnswer = new List<string> { "الميم", "الكاف", "اللام" }
        }
    },
    imageLink = "https://assets.rbl.ms/19152954/origin.jpg"
};
            return Ok(lesson);*/
    }

        // Get a lesson by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Get(string id)
        {
            var lesson = await _lessonsCollection.Find(lesson => lesson.id == id).FirstOrDefaultAsync();
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }

        // Create a new lesson

        [HttpPost]
        public async Task<ActionResult<Lesson>> Create(Lesson lesson)
        {
            await _lessonsCollection.InsertOneAsync(lesson);
            return CreatedAtAction(nameof(Get), new { id = lesson.id }, lesson);
        }

        // Update a lesson
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Lesson updatedLesson)
        {
            var result = await _lessonsCollection.ReplaceOneAsync(lesson => lesson.id == id, updatedLesson);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete a lesson
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _lessonsCollection.DeleteOneAsync(lesson => lesson.id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}