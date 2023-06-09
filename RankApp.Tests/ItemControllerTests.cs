using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RankApp.Controllers;
using RankApp.Models;
using Xunit;

namespace RankApp.Tests
{
    public class ItemControllerTests
    {
        private readonly ItemController _itemController;
        private readonly Mock<ILogger<ItemController>> _mockLogger;
        private readonly AppDbContext _mockDbContext;

        public ItemControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // TestDatabase can be any string
                .Options;

            _mockDbContext = new AppDbContext(options);
            _mockLogger = new Mock<ILogger<ItemController>>();
            _itemController = new ItemController(_mockDbContext, _mockLogger.Object);
        }

        [Fact]
        public void Get_WhenCalledWithValidItemType_ReturnsOkResult()
        {
            // Arrange
            var itemType = "testType";
            _mockDbContext.Items.Add(
                new ItemModel
                {
                    Id = 0,
                    Title = "Test",
                    Ranking = 1,
                    ItemType = itemType
                }
            );
            _mockDbContext.SaveChanges();

            // Act
            var okResult = _itemController.Get(itemType);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalledWithNonExistentItemType_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var itemType = "nonexistentType";

            // Act
            var okResult = _itemController.Get(itemType) as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Empty(okResult.Value as IEnumerable<object>);
        }

        [Fact]
        public void Post_WhenValidItem_ReturnsCreatedAtAction()
        {
            // Arrange
            var newItem = new ItemModel
            {
                Id = 0,
                Title = "Test Item",
                ItemType = "Test Type"
            };

            // Act
            var result = _itemController.Post(newItem) as CreatedAtActionResult;
            // Console.WriteLine(result);



            Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(result);
            Assert.Equal(newItem, result.Value);
        }

        [Fact]
        public void Post_WhenMissingRequiredFields_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new ItemModel(); // Missing required fields

            // Act
            var result = _itemController.Post(newItem);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Title and ItemType are required fields.", badRequestResult.Value);
        }

        [Fact]
        public void UpdateItemRanking_WithValidIdAndUpdatedItem_ReturnsOkResult()
        {
            // Arrange
            int itemId = 1;
            var existingItem = new ItemModel { Id = itemId, Ranking = 5 };
            _mockDbContext.Items.Add(existingItem);
            _mockDbContext.SaveChanges();

            var updatedItem = new ItemModel { Id = itemId, Ranking = 8 };

            // Act
            var result = _itemController.UpdateItemRanking(itemId, updatedItem);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedItemResult = Assert.IsType<ItemModel>(okResult.Value);

            Assert.Equal(itemId, updatedItemResult.Id);
            Assert.Equal(updatedItem.Ranking, updatedItemResult.Ranking);
        }

        [Fact]
        public void UpdateItemRanking_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int itemId = 30;
            var updatedItem = new ItemModel { Id = itemId, Ranking = 8 };

            // Act
            var result = _itemController.UpdateItemRanking(itemId, updatedItem);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ResetRankings_WhenItemsExist_ReturnsOkResult()
        {
            // Arrange
            var item1 = new ItemModel { Id = 33, Ranking = 5 };
            var item2 = new ItemModel { Id = 34, Ranking = 8 };

            _mockDbContext.Items.Add(item1);
            _mockDbContext.Items.Add(item2);
            _mockDbContext.SaveChanges();

            // Act
            var result = _itemController.ResetRankings();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rankings reset successfully.", okResult.Value);
        }

        [Fact]
        public void ResetRankings_WhenNoItemsExist_ReturnsOkResult()
        {
            // Act
            var result = _itemController.ResetRankings();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rankings reset successfully.", okResult.Value);
        }

        [Fact]
        public void ResetRankings_WithException_ReturnsStatusCode500()
        {
            // Arrange
            var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            mockDbContext.Setup(db => db.SaveChanges()).Throws(new Exception("Test exception"));

            var itemController = new ItemController(mockDbContext.Object, _mockLogger.Object);

            // Act
            var result = itemController.ResetRankings();

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
