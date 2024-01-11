using Arabic_Arena.Config;
using Arabic_Arena.Controllers;
using Arabic_Arena.Models;
using Arabic_Arena.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace Arabic_Arena.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IMongoCollection<User>> _mockUsersCollection;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUsersCollection = new Mock<IMongoCollection<User>>();
            var builder = WebApplication.CreateBuilder();
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var context = new MongoDbContext(mongoDbSettings);
            _controller = new UsersController(context);
        }

        [Test]
        public async Task Get_ReturnsListOfUsers()
        {
            // Arrange
            var mockUsers = new List<User>
    {
        new User { id = "1", firstName = "John" },
        new User { id = "2", firstName = "Jane" }
    };

            _mockUsersCollection.Setup(collection => collection.FindAsync(It.IsAny<FilterDefinition<User>>(), null, default))
                .ReturnsAsync(new Mock<IAsyncCursor<User>>().Object);

            var cursor = new Mock<IAsyncCursor<User>>();
            cursor.Setup(_ => _.Current).Returns(mockUsers);
            cursor.SetupSequence(_ => _.MoveNextAsync(default))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockUsersCollection.Setup(_ => _.FindSync(It.IsAny<FilterDefinition<User>>(), null, default))
                .Returns(cursor.Object);
            // Act
            var result = await _controller.Get();
            // Assert
            var okResult = result.Result as OkObjectResult;
            var users = okResult.Value as List<User>;
            Assert.Equals(2, users.Count);
            Assert.Equals("1", users[0].id);
            Assert.Equals("John", users[0].firstName);
            Assert.Equals("2", users[1].id);
            Assert.Equals("Jane", users[1].firstName);
        }
        /*[Test]
        public async Task Get_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            string invalidId = "invalid_id";
            _mockUsersCollection.Setup(collection => collection.Find(It.IsAny<Expression<Func<User, bool>>>()).FirstOrDefaultAsync()).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Get(invalidId);
            
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
        */
        // Additional tests for Get(string id), Create(User user), Update(string id, User updatedUser), GetUsersCount()
    }

}

