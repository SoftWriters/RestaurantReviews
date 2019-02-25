using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReviews.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DomainData");

            migrationBuilder.CreateTable(
                name: "Restaurants",
                schema: "DomainData",
                columns: table => new
                {
                    RestaurantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 11, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    WebsiteUrl = table.Column<string>(maxLength: 200, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 60, nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "DomainData",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    RestaurauntId = table.Column<Guid>(nullable: false),
                    SubmissionDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "DomainData",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                schema: "DomainData",
                table: "Restaurants",
                columns: new[] { "RestaurantId", "Address", "City", "Country", "EmailAddress", "IsConfirmed", "Name", "Phone", "PostalCode", "State", "WebsiteUrl" },
                values: new object[,]
                {
                    { new Guid("0ee5ad3e-1e1e-467a-9ecb-971a1e3aec7f"), "Park Drive", "Niles", "USA", "", true, "Bombay Curry and Grill", "(330) 544-4444", "44446", "OH", "https://www.doordash.com/store/bombay-curry-grill-niles-403108/" },
                    { new Guid("e5e972bb-7968-466e-b493-dd05948a7fea"), "5555 Youngstown Warren Rd #175", "Niles", "USA", "", true, "Mizu Japanese Restaurant - Niles", "(330) 652-2888", "44446", "OH", "http://www.mizu-oh.com/" },
                    { new Guid("2c2cbc01-09e7-4847-81ef-7548cdf0a01a"), "824 N State St", "Girard", "USA", "", true, "The Daily Grind, Girard", "(234) 421-5118", "44420", "OH", "http://www.thedailygrindgirard.com/" }
                });

            migrationBuilder.InsertData(
                schema: "DomainData",
                table: "Reviews",
                columns: new[] { "ReviewId", "Comment", "Rating", "RestaurauntId", "SubmissionDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("16df3133-95d2-472a-9c3e-2ce0df5acccd"), "The food was excellent.", 4, new Guid("0ee5ad3e-1e1e-467a-9ecb-971a1e3aec7f"), new DateTime(2019, 2, 22, 5, 5, 23, 9, DateTimeKind.Utc).AddTicks(8826), new Guid("8555ff24-7bc7-47a2-9bff-29631386e04f") },
                    { new Guid("85f490b2-a3f2-423c-b80b-8557e937c5fc"), "The food was good.", 3, new Guid("e5e972bb-7968-466e-b493-dd05948a7fea"), new DateTime(2019, 2, 22, 5, 5, 23, 10, DateTimeKind.Utc).AddTicks(2176), new Guid("8555ff24-7bc7-47a2-9bff-29631386e04f") },
                    { new Guid("72226d68-e160-47e0-96b7-a27fa40fce20"), "Great food and great service.", 5, new Guid("2c2cbc01-09e7-4847-81ef-7548cdf0a01a"), new DateTime(2019, 2, 22, 5, 5, 23, 10, DateTimeKind.Utc).AddTicks(2191), new Guid("8555ff24-7bc7-47a2-9bff-29631386e04f") },
                    { new Guid("1be5046a-ab20-40f9-b678-9b0db543e974"), "Excellent coffee.", 5, new Guid("2c2cbc01-09e7-4847-81ef-7548cdf0a01a"), new DateTime(2019, 2, 22, 5, 5, 23, 10, DateTimeKind.Utc).AddTicks(2193), new Guid("aa7bd40c-b52c-4979-8645-8332e5bca63a") }
                });

            migrationBuilder.InsertData(
                schema: "DomainData",
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "EmailAddress", "FirstName", "IsActive", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { new Guid("8555ff24-7bc7-47a2-9bff-29631386e04f"), new DateTime(2019, 2, 22, 5, 5, 23, 8, DateTimeKind.Utc).AddTicks(7306), "user1@email.com", "1", true, "User1", null },
                    { new Guid("aa7bd40c-b52c-4979-8645-8332e5bca63a"), new DateTime(2019, 2, 22, 5, 5, 23, 8, DateTimeKind.Utc).AddTicks(9246), "user2@email.com", "2", true, "User2", null },
                    { new Guid("746cf42d-d282-45ec-8d91-7b3858f1876a"), new DateTime(2019, 2, 22, 5, 5, 23, 8, DateTimeKind.Utc).AddTicks(9260), "user3@email.com", "3", true, "User3", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants",
                schema: "DomainData");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "DomainData");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "DomainData");
        }
    }
}
