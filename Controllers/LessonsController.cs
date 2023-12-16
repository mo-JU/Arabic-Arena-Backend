using Arabic_Arena.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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