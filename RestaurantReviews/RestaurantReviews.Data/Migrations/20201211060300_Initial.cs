using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReviews.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "DateModified", "First", "Last" },
                values: new object[,]
                {
                    { new Guid("43757037-6429-4aa0-8c96-62867c419967"), new DateTime(2020, 12, 11, 6, 2, 59, 680, DateTimeKind.Utc).AddTicks(8602), new DateTime(2020, 12, 11, 6, 2, 59, 680, DateTimeKind.Utc).AddTicks(8637), "Homer", "Simpson" },
                    { new Guid("ac6b3f45-2cdf-4a2d-8df1-764f25ca8614"), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(811), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(819), "Marge", "Simpson" },
                    { new Guid("41355f76-973d-47cd-9c48-90ae826d9a0c"), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(879), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(881), "Bart", "Simpson" },
                    { new Guid("779ab260-7af2-421a-b0cf-4df17164a406"), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(888), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(889), "Lisa", "Simpson" },
                    { new Guid("292801c5-a3ca-4f35-9224-159ec9667e11"), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(896), new DateTime(2020, 12, 11, 6, 2, 59, 681, DateTimeKind.Utc).AddTicks(897), "Maggie", "Simpson" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
