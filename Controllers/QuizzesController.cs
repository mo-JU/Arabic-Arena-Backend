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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> Get()
        {
            var projection = Builders<Quiz>.Projection
                .Include(l => l.id)
                .Include(l => l.titleArabic)
                .Include(l => l.titleEnglish)
                .Include(l => l.type)
                .Include(l => l.level);

            var quizzes = await _quizzesCollection.Find(_ => true)
                .Project<Quiz>(projection)
                .ToListAsync();

            return Ok(quizzes);
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
    }
}
