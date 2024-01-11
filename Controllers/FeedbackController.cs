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
    public class FeedbackController: ControllerBase
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;
        public FeedbackController(MongoDbContext context)
        {
            _feedbackCollection = context.Feedback;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get()
        {
            var lessons = await _feedbackCollection.Find(_ => true).ToListAsync();

            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> Create(Feedback feedback)
        {
            await _feedbackCollection.InsertOneAsync(feedback);
            return CreatedAtAction(nameof(Get), new { id = feedback.id }, feedback);
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetFeedbackCount()
        {
            var count = await _feedbackCollection.CountDocumentsAsync(_ => true);
            return Ok(count);
        }
    }
}
