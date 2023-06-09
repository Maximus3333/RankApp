using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RankApp.Models;
using Microsoft.Extensions.Logging;

namespace RankApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ItemController> _logger;

        public ItemController(AppDbContext dbContext, ILogger<ItemController> logger)
        {
            _logger = logger;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("{itemType}")]
        public IActionResult Get(string itemType)
        {
            try
            {
                _logger.LogInformation("Get action is being hit.");

                ItemModel[] items = _dbContext.Items.Where(i => i.ItemType == itemType).ToArray();

                var responseItems = items.Select(item =>
                {
                    string? imageString = null;

                    if (item.Image != null)
                    {
                        // Convert the image byte array to base64 string representation
                        imageString = Convert.ToBase64String(item.Image);
                    }

                    return new
                    {
                        item.Id,
                        item.Title,
                        item.Ranking,
                        item.ItemType,
                        ImageBase64 = imageString,
                        ImageUrl = item.ImageUrl ?? string.Empty // Handle nullable ImageUrl
                    };
                });

                return Ok(responseItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(ItemModel newItem)
        {
            try
            {
                _logger.LogInformation("Post action is being hit.");

                // Check if all the required fields are filled out
                if (string.IsNullOrEmpty(newItem.Title) || string.IsNullOrEmpty(newItem.ItemType))
                {
                    return BadRequest("Title and ItemType are required fields.");
                }

                // Perform any additional validation or business logic checks if needed

                // Set the default values
                newItem.Id = 0; // Assuming it's an auto-increment field in the database

                // Add the new item to the database


                _dbContext.Items.Add(newItem);
                _dbContext.SaveChanges();

                // Return the newly added item with a 201 Created status
                return CreatedAtAction(nameof(Get), new { itemType = newItem.ItemType }, newItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItemRanking(int id, [FromBody] ItemModel updatedItem)
        {
            try
            {
                _logger.LogInformation("UpdateItemRanking action is being hit.");

                // Find the item with the specified id in the database
                var existingItem = _dbContext.Items.FirstOrDefault(item => item.Id == id);

                // Check if the item exists
                if (existingItem == null)
                {
                    return NotFound();
                }

                // Update the ranking of the item
                existingItem.Ranking = updatedItem.Ranking;

                // Save the changes to the database
                _dbContext.SaveChanges();

                // Return the updated item with a 200 OK status
                return Ok(existingItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("reset-rankings")]
        public IActionResult ResetRankings()
        {
            try
            {
                _logger.LogInformation("ResetRankings action is being hit.");

                // Get all items from the database
                var items = _dbContext.Items.ToList();

                // Update the rankings for each item
                foreach (var item in items)
                {
                    item.Ranking = 0;
                }

                // Save the changes to the database
                _dbContext.SaveChanges();

                // Return a success response
                return Ok("Rankings reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        
    }
}
