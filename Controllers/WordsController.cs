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

    public class WordsController:ControllerBase
    {
            private readonly IMongoCollection<Word> _wordsCollection;

            public WordsController(MongoDbContext context)
            {
                _wordsCollection = context.Words;
            }

            [HttpGet("id")]
            public async Task<ActionResult<Word>> Get(string id)
            {
                var word = await _wordsCollection.Find(word => word.id == id).FirstOrDefaultAsync();
                if (word == null)
                {
                    return NotFound();
                }
                return Ok(word);
            }

            [HttpPost]
            public async Task<ActionResult<Word>> Create(Word word)
            {
                await _wordsCollection.InsertOneAsync(word);
                return CreatedAtAction(nameof(Get), new { id=word.id }, word);
            }
    }
}
