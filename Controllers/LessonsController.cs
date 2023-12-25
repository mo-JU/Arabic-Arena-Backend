using Arabic_Arena.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Arabic_Arena.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("arabicarena")]

    public class LessonsController : ControllerBase
    {
        private readonly IMongoCollection<Lesson> _lessonsCollection;

        public LessonsController(MongoDbContext context)
        {
            _lessonsCollection = context.Lessons; // Replace with your actual collection name
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get()
        {
            var projection = Builders<Lesson>.Projection
                .Include(l => l.id)
                .Include(l => l.titleArabic)
                .Include(l => l.titleEnglish)
                .Include(l => l.type)
                .Include(l => l.level)
                .Include(l => l.imageLink);
                
            var lessons = await _lessonsCollection.Find( _ => true)
                .Project<Lesson>(projection)
                .ToListAsync();

            return Ok(lessons);
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