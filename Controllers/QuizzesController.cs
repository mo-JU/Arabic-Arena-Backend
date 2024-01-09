using Arabic_Arena.Models;
using Arabic_Arena.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Arabic_Arena.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("arabicarena")]

    public class QuizzesController : ControllerBase
    {
        private readonly IMongoCollection<Quiz> _quizzesCollection;

        public QuizzesController(MongoDbContext context)
        {
            _quizzesCollection = context.Quizzes;
        }

        public class QuizDto
        {
            public string id { get; set; }
            public string titleArabic { get; set; }
            public string titleEnglish { get; set; }
            public string level { get; set; }
            public string type { get; set; }
            public int time { get; set; }
            public string imageLink { get; set; }
            public int QuestionsCount { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get()
        {
            var quizzes = await _quizzesCollection.Find(_ => true).ToListAsync();
            quizzes.Reverse();

            var quizzesDto = quizzes.Select(q => new QuizDto
            {
                id = q.id,
                titleArabic = q.titleArabic,
                titleEnglish = q.titleEnglish,
                level = q.level,
                type = q.type,
                time = q.time,
                imageLink = q.imageLink,
                QuestionsCount = q.questions?.Count ?? 0
            });

            return Ok(quizzesDto);
        }

        [HttpGet("{titleEnglish}")]
        public async Task<ActionResult<Quiz>> Get(string titleEnglish)
        {
            var quiz = await _quizzesCollection.Find(quiz => quiz.titleEnglish.ToLower() == titleEnglish.ToLower()).FirstOrDefaultAsync();
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> Create(Quiz quiz)
        {
            await _quizzesCollection.InsertOneAsync(quiz);
            return CreatedAtAction(nameof(Get), new { id = quiz.id }, quiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Quiz updatedQuiz)
        {
            var result = await _quizzesCollection.ReplaceOneAsync(quiz => quiz.id == id, updatedQuiz);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _quizzesCollection.DeleteOneAsync(quiz => quiz.id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("count")]
        public async Task<ActionResult<long>> GetQuizzesCount()
        {
            var count = await _quizzesCollection.CountDocumentsAsync( _ => true);
            return Ok(count);
        }
    }
}
