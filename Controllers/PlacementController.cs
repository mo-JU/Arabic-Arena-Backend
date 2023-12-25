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

    public class PlacementController : ControllerBase
    {
        private readonly IMongoCollection<PlacementTest> _placementCollection;

        public PlacementController(MongoDbContext context)
        {
            _placementCollection = context.Placement;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlacementTest>> Get(string id)
        {
            var test = await _placementCollection.Find(test => test.id == id).FirstOrDefaultAsync();
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PlacementTest updatedTest)
        {
            var result = await _placementCollection.ReplaceOneAsync(test => test.id == id, updatedTest);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        
    }
}
