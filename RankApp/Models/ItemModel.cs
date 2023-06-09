using System;
namespace RankApp.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Ranking { get; set; }

        public string ItemType { get; set; }

        public byte[]? Image { get; set; }

		public string? ImageUrl { get; set; }

        public ItemModel()
    {
        Title = string.Empty;
        ItemType = string.Empty;
    }
    }


    
}
