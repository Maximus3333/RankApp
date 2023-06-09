using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RankApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Title = table.Column<string>(type: "TEXT", nullable: false),
                        ImageId = table.Column<int>(type: "INTEGER", nullable: false),
                        Ranking = table.Column<int>(type: "INTEGER", nullable: false),
                        ItemType = table.Column<string>(type: "TEXT", nullable: false),
                        Image = table.Column<byte[]>(type: "BLOB", nullable: true),
                        ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                }
            );
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Title", "ImageId", "Ranking", "ItemType", "Image" },
                values: new object[,]
                {
                    { 1, "The Godfather", 1, 0, "movie", ReadImageFromFile("Godfather.png") },
                    { 2, "Highlander", 2, 0, "movie", ReadImageFromFile("Highlander.png") },
                    { 3, "Highlander II", 3, 0, "movie", ReadImageFromFile("Highlander2.png") },
                    {
                        4,
                        "The Last of the Mohicans",
                        4,
                        0,
                        "movie",
                        ReadImageFromFile("LastOfTheMohicans.png")
                    },
                    {
                        5,
                        "Police Academy 6",
                        5,
                        0,
                        "movie",
                        ReadImageFromFile("PoliceAcademy6.png")
                    },
                    { 6, "Rear Window", 6, 0, "movie", ReadImageFromFile("RearWindow.png") },
                    { 7, "Road House", 7, 0, "movie", ReadImageFromFile("RoadHouse.png") },
                    {
                        8,
                        "The Shawshank Redemption",
                        8,
                        0,
                        "movie",
                        ReadImageFromFile("Shawshank.png")
                    },
                    { 9, "Star Treck IV", 9, 0, "movie", ReadImageFromFile("StarTreck4.png") },
                    { 10, "Superman 4", 10, 0, "movie", ReadImageFromFile("Superman4.png") },
                    { 11, "Abbey Road", 11, 0, "album", ReadImageFromFile("AbbeyRoad.png") },
                    { 12, "Adrenalize", 12, 0, "album", ReadImageFromFile("Adrenalize.png") },
                    { 13, "Back in Black", 13, 0, "album", ReadImageFromFile("BackInBlack.png") },
                    {
                        14,
                        "Enjoy the Silence",
                        14,
                        0,
                        "album",
                        ReadImageFromFile("EnjoyTheSilence.png")
                    },
                    { 15, "Parachutes", 15, 0, "album", ReadImageFromFile("Parachutes.png") },
                    {
                        16,
                        "Ride the Lightning",
                        16,
                        0,
                        "album",
                        ReadImageFromFile("RideTheLightning.png")
                    },
                    { 17, "Rock or Bust", 17, 0, "album", ReadImageFromFile("RockOrBust.png") },
                    { 18, "Rust in Peace", 18, 0, "album", ReadImageFromFile("RustInPeace.png") },
                    { 19, "St. Anger", 19, 0, "album", ReadImageFromFile("StAnger.png") },
                    {
                        20,
                        "The Final Countdown",
                        20,
                        0,
                        "album",
                        ReadImageFromFile("TheFinalCountdown.png")
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Items");
        }

        private byte[] ReadImageFromFile(string fileName)
        {
            string imagePath = Path.Combine("images", fileName);
            return File.ReadAllBytes(imagePath);
        }

        /// <inheritdoc />
    }
}
