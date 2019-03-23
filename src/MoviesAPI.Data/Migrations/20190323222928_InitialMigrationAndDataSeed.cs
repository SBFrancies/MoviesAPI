using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesAPI.Data.Migrations
{
    public partial class InitialMigrationAndDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    YearOfRelease = table.Column<int>(nullable: false),
                    RunningTime = table.Column<int>(nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false),
                    Rating = table.Column<decimal>(nullable: false),
                    DateSet = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Ratings",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Ratings",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Genre", "RunningTime", "Title", "YearOfRelease" },
                values: new object[,]
                {
                    { new Guid("63350e4a-a5fb-42bd-83da-f75f7b8807b4"), 8194, 200, "Titanic", 2000 },
                    { new Guid("2e3dc0d2-b37c-490e-912c-b8b150b716ac"), 2048, 87, "Man on Wire", 2010 },
                    { new Guid("49f63645-60ed-4651-9ed8-51536f553981"), 16, 111, "Saw", 2003 },
                    { new Guid("ab7be0f3-e394-4c98-a80c-8a94ba62bf2f"), 6, 120, "Batman", 1989 },
                    { new Guid("f7b47d60-3232-426d-a1aa-833aef7bb676"), 5, 123, "Iron Man 3", 2013 },
                    { new Guid("dc6b3d4d-959b-4cef-a7eb-f92731811b95"), 5, 118, "Iron Man", 2007 },
                    { new Guid("37498bd4-d693-4ce0-933c-6cc8f9377ae9"), 1025, 93, "The Simpsons Movie", 2009 },
                    { new Guid("81aa0a92-70fb-4ffc-860b-a81ca47be098"), 5, 145, "Iron Man 2", 2010 },
                    { new Guid("b839d79d-a339-416d-ade0-936edbcf6691"), 8192, 127, "The Dutchess", 2006 },
                    { new Guid("ead5e981-2125-47a6-a0a6-b4d6ffa14339"), 129, 98, "27 Dresses", 2011 },
                    { new Guid("560c0dbe-6927-41a1-8d8d-0789c4c9851d"), 17, 110, "Get Out", 2018 },
                    { new Guid("1abaadad-a1ee-403d-a17f-31321ec5859f"), 32, 631, "The Lord of the Rings", 2007 },
                    { new Guid("61adc654-6f05-4aee-914e-6c7701e6ec76"), 4, 120, "Die Hard", 1999 },
                    { new Guid("ca2a8812-e099-495f-b036-06accb4355a7"), 12, 163, "Casino Royale", 2004 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f"), new DateTimeOffset(new DateTime(1973, 3, 23, 22, 29, 27, 466, DateTimeKind.Unspecified).AddTicks(2582), new TimeSpan(0, 0, 0, 0, 0)), "Leon", "Smith" },
                    { new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2"), new DateTimeOffset(new DateTime(2001, 3, 23, 22, 29, 27, 466, DateTimeKind.Unspecified).AddTicks(1721), new TimeSpan(0, 0, 0, 0, 0)), "Tom", "Smith" },
                    { new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c"), new DateTimeOffset(new DateTime(1947, 3, 23, 22, 29, 27, 466, DateTimeKind.Unspecified).AddTicks(2568), new TimeSpan(0, 0, 0, 0, 0)), "Jamal", "Jones" },
                    { new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594"), new DateTimeOffset(new DateTime(1991, 3, 23, 22, 29, 27, 466, DateTimeKind.Unspecified).AddTicks(2577), new TimeSpan(0, 0, 0, 0, 0)), "John", "Robertson" },
                    { new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c"), new DateTimeOffset(new DateTime(1987, 3, 23, 22, 29, 27, 466, DateTimeKind.Unspecified).AddTicks(2582), new TimeSpan(0, 0, 0, 0, 0)), "Katie", "McDonald" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "DateSet", "MovieId", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("e4e1529d-e2f9-45d6-8e86-4d404908ec1c"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 481, DateTimeKind.Unspecified).AddTicks(8747), new TimeSpan(0, 0, 0, 0, 0)), new Guid("63350e4a-a5fb-42bd-83da-f75f7b8807b4"), 5m, new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2") },
                    { new Guid("e5ccfdce-d8e1-4fd0-943b-f57f5fefbc0b"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, 0, 0, 0, 0)), new Guid("dc6b3d4d-959b-4cef-a7eb-f92731811b95"), 7.5m, new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c") },
                    { new Guid("637677af-90e0-4ed0-9256-4c39d0c17151"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(190), new TimeSpan(0, 0, 0, 0, 0)), new Guid("37498bd4-d693-4ce0-933c-6cc8f9377ae9"), 7m, new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c") },
                    { new Guid("41a2a170-777d-437c-8792-b027313b9e73"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(185), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ca2a8812-e099-495f-b036-06accb4355a7"), 2m, new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c") },
                    { new Guid("2e5ca995-a048-46aa-b075-e8a7afc2af2b"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(185), new TimeSpan(0, 0, 0, 0, 0)), new Guid("b839d79d-a339-416d-ade0-936edbcf6691"), 9.5m, new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f") },
                    { new Guid("69a1439b-b6bc-4f47-9a12-705bea3cfcb5"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(180), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ead5e981-2125-47a6-a0a6-b4d6ffa14339"), 4.5m, new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f") },
                    { new Guid("da3af618-d1f0-4840-b3cb-87dd5a239cf7"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(180), new TimeSpan(0, 0, 0, 0, 0)), new Guid("560c0dbe-6927-41a1-8d8d-0789c4c9851d"), 3m, new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f") },
                    { new Guid("f902ae8e-0ca2-49c3-8f95-8a79fb581c11"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(176), new TimeSpan(0, 0, 0, 0, 0)), new Guid("1abaadad-a1ee-403d-a17f-31321ec5859f"), 2m, new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f") },
                    { new Guid("472edd2e-c60b-4ae8-9a34-cb8c8ade64eb"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(176), new TimeSpan(0, 0, 0, 0, 0)), new Guid("61adc654-6f05-4aee-914e-6c7701e6ec76"), 10m, new Guid("022b41a2-7e40-4104-98a9-7cd8dcc67a0f") },
                    { new Guid("625d2f78-50ac-4d41-bfaf-790a444add7c"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(171), new TimeSpan(0, 0, 0, 0, 0)), new Guid("63350e4a-a5fb-42bd-83da-f75f7b8807b4"), 9m, new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594") },
                    { new Guid("9052b80e-712d-4c8b-b3da-cfac0041cf1e"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(171), new TimeSpan(0, 0, 0, 0, 0)), new Guid("2e3dc0d2-b37c-490e-912c-b8b150b716ac"), 8m, new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594") },
                    { new Guid("e4f614b9-466a-4519-a60f-5a08c517e963"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, 0, 0, 0, 0)), new Guid("81aa0a92-70fb-4ffc-860b-a81ca47be098"), 5m, new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c") },
                    { new Guid("8bb47064-2bf7-4f2a-9c21-1f529698f182"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(143), new TimeSpan(0, 0, 0, 0, 0)), new Guid("49f63645-60ed-4651-9ed8-51536f553981"), 7m, new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594") },
                    { new Guid("79b9cac7-29de-4af0-9216-216d7943a447"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(138), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f7b47d60-3232-426d-a1aa-833aef7bb676"), 8.5m, new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594") },
                    { new Guid("2dbe0a99-9918-45f0-b154-6f51ed7777a9"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(134), new TimeSpan(0, 0, 0, 0, 0)), new Guid("81aa0a92-70fb-4ffc-860b-a81ca47be098"), 5.5m, new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c") },
                    { new Guid("4cbd91fe-ae0f-4013-9eb8-cd5a760b0011"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(134), new TimeSpan(0, 0, 0, 0, 0)), new Guid("dc6b3d4d-959b-4cef-a7eb-f92731811b95"), 9m, new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c") },
                    { new Guid("ca9e082e-445a-4c78-8df6-1d34487f7b5a"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(129), new TimeSpan(0, 0, 0, 0, 0)), new Guid("37498bd4-d693-4ce0-933c-6cc8f9377ae9"), 3m, new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c") },
                    { new Guid("0f4b7501-38c8-458f-ad04-79555c9acd47"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(129), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ca2a8812-e099-495f-b036-06accb4355a7"), 6m, new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c") },
                    { new Guid("73b1143f-836b-4967-8696-f85f93a456c6"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(125), new TimeSpan(0, 0, 0, 0, 0)), new Guid("b839d79d-a339-416d-ade0-936edbcf6691"), 1m, new Guid("86e4da8a-6ea9-416a-af9b-c037249c8f3c") },
                    { new Guid("eadb0134-9b07-4e18-9402-dd5d99a4684b"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(111), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ead5e981-2125-47a6-a0a6-b4d6ffa14339"), 8m, new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2") },
                    { new Guid("40b1fca2-4b54-495f-97f8-0706035766fd"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(106), new TimeSpan(0, 0, 0, 0, 0)), new Guid("560c0dbe-6927-41a1-8d8d-0789c4c9851d"), 2.5m, new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2") },
                    { new Guid("1de965cc-af7d-434e-b999-2ce6273b4d89"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(106), new TimeSpan(0, 0, 0, 0, 0)), new Guid("1abaadad-a1ee-403d-a17f-31321ec5859f"), 5.5m, new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2") },
                    { new Guid("e8c18505-134f-4e49-8b3a-4748ca9dde7e"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(92), new TimeSpan(0, 0, 0, 0, 0)), new Guid("61adc654-6f05-4aee-914e-6c7701e6ec76"), 9m, new Guid("8d61bd5e-d9b3-4eda-822c-d7ce257abbc2") },
                    { new Guid("237a56aa-6657-40bc-884f-d8388a4467b8"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(138), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ab7be0f3-e394-4c98-a80c-8a94ba62bf2f"), 6m, new Guid("b18a8a2d-0369-48a4-8ca8-8af187487594") },
                    { new Guid("9e7450af-4e1e-4e4b-bd85-a8272d644d27"), new DateTimeOffset(new DateTime(2019, 3, 23, 22, 29, 27, 482, DateTimeKind.Unspecified).AddTicks(199), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f7b47d60-3232-426d-a1aa-833aef7bb676"), 6m, new Guid("db08b3be-859a-4012-914f-4bb4f5aad27c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
