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
    public class UsersController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersController(MongoDbContext context)
        {
            _usersCollection = context.Users;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var projection = Builders<User>.Projection
                .Include(u => u.id)
                .Include(u => u.firstName)
                .Include(u => u.lastName)
                .Include(u => u.level)
                .Include(u => u.language);

            var users = await _usersCollection.Find(_ => true)
                .Project<User>(projection)
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _usersCollection.Find(user => user.id.ToLower() == id.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var result = await _usersCollection.ReplaceOneAsync(user => user.id == id, updatedUser);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetUsersCount()
        {
            var count = await _usersCollection.CountDocumentsAsync(_ => true);
            return Ok(count);
        }
    }
}
