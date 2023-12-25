using Arabic_Arena.Config;
using Arabic_Arena.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Arabic_Arena.Services
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase("ArabicArenaDB"); 
        }

        public IMongoCollection<Lesson> Lessons => _database.GetCollection<Lesson>("Lessons");
        public IMongoCollection<Word> Words => _database.GetCollection<Word>("Words");
        public IMongoCollection<Quiz> Quizzes => _database.GetCollection<Quiz>("Quiz");
        public IMongoCollection<PlacementTest> Placement => _database.GetCollection<PlacementTest>("Placement");
        public IMongoCollection<Admin> Admin => _database.GetCollection<Admin>("Admin");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    }
}
