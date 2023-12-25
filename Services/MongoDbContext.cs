﻿using Arabic_Arena.Config;
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
            _database = client.GetDatabase("ArabicArenaDB"); // Replace with your actual database name
        }

        // You can add methods here to access collections, perform CRUD operations, etc.
        public IMongoCollection<Lesson> Lessons => _database.GetCollection<Lesson>("Lessons");
        public IMongoCollection<Word> Words => _database.GetCollection<Word>("Words");
    }
}
