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

    public class AdminController : ControllerBase
    {
        private readonly IMongoCollection<Admin> _adminCollection;

        public AdminController(MongoDbContext context)
        {
            _adminCollection = context.Admin;
        }

        [HttpPost]
        public async Task<ActionResult<Admin>> Validate(Admin user)
        {
            var admin = await _adminCollection.Find(admin => admin.id == user.id).FirstOrDefaultAsync();
            if (admin == null || !(HashingServices.VerifyPassword(admin.password,user.password)))
            {
                return Unauthorized();
            }
            return Ok(admin);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ChangePassword user)
        {
            var admin = await _adminCollection.Find(admin => admin.id == id).FirstOrDefaultAsync();
            if (admin == null || !(HashingServices.VerifyPassword(admin.password, user.password)))
            {
                return Unauthorized();
            }
            admin.password = HashingServices.HashPassword(user.newPassword);
            var result = await _adminCollection.ReplaceOneAsync(admin => admin.id == id,admin);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
