using Arabic_Arena.Controllers;
using Arabic_Arena.Models;
using Arabic_Arena.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
/*
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
            // Mock other necessary parts of MongoDbContext if needed

            var context = new Mock<MongoDbContext>(); // Replace with actual context or mock
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

            _mockUsersCollection.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockUsers);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var users = okResult.Value as IEnumerable<User>;
            Assert.AreEqual(2, users.Count());
        }

        // Additional tests for Get(string id), Create(User user), Update(string id, User updatedUser), GetUsersCount()
    }

}

*/