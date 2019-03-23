using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesAPI.Data.Migrations
{
    public partial class InitialMigration : Migration
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
                    { new Guid("7221cc40-a577-47c5-bee5-e5c39e068c54"), 8194, 200, "Titanic", 2000 },
                    { new Guid("793f32ad-9166-4fac-8603-4da67800e5a1"), 2048, 87, "Man on Wire", 2010 },
                    { new Guid("4db8b514-a4c1-4f81-b89d-04f53f573586"), 16, 111, "Saw", 2003 },
                    { new Guid("9934399e-40f7-4a63-aa93-000555aa35d7"), 6, 120, "Batman", 1989 },
                    { new Guid("bea6446a-390f-4f42-9486-30603aeb5c17"), 5, 123, "Iron Man 3", 2013 },
                    { new Guid("8e787d4f-a156-46b8-8545-01ea93dc866b"), 5, 118, "Iron Man", 2007 },
                    { new Guid("98fd020f-7568-4102-8d77-445b749c9d9b"), 1025, 93, "The Simpsons Movie", 2009 },
                    { new Guid("2d8f2a2d-300d-41fb-a725-0a3f48c2591c"), 5, 145, "Iron Man 2", 2010 },
                    { new Guid("4a6bbe9e-5486-4c39-8cd8-582cfd89e1d1"), 8192, 127, "The Dutchess", 2006 },
                    { new Guid("98b9067a-caa1-46b1-b07e-850e60ea1df7"), 129, 98, "27 Dresses", 2011 },
                    { new Guid("1e8b0d25-c644-47fa-b2a5-24a95fb84138"), 17, 110, "Get Out", 2018 },
                    { new Guid("8fd45d63-9ab1-400d-b4de-a7e96dbebf4a"), 32, 631, "The Lord of the Rings", 2007 },
                    { new Guid("911b09fb-d9ab-49c3-802f-44daeade3a4a"), 4, 120, "Die Hard", 1999 },
                    { new Guid("aeb5174c-e518-491d-8124-fe7e19375d20"), 12, 163, "Casino Royale", 2004 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("47aa2c04-7ccf-44e9-b528-bc88044171bf"), new DateTimeOffset(new DateTime(1973, 3, 23, 2, 47, 20, 935, DateTimeKind.Unspecified).AddTicks(8509), new TimeSpan(0, 0, 0, 0, 0)), "Leon", "Smith" },
                    { new Guid("23a47e06-f104-4a03-96b3-731de11d24cb"), new DateTimeOffset(new DateTime(2001, 3, 23, 2, 47, 20, 935, DateTimeKind.Unspecified).AddTicks(7620), new TimeSpan(0, 0, 0, 0, 0)), "Tom", "Smith" },
                    { new Guid("18607aca-a79d-4a00-a4ae-18041ec50b01"), new DateTimeOffset(new DateTime(1947, 3, 23, 2, 47, 20, 935, DateTimeKind.Unspecified).AddTicks(8495), new TimeSpan(0, 0, 0, 0, 0)), "Jamal", "Jones" },
                    { new Guid("a458d790-5e57-45ee-a738-0ae6146d520f"), new DateTimeOffset(new DateTime(1991, 3, 23, 2, 47, 20, 935, DateTimeKind.Unspecified).AddTicks(8504), new TimeSpan(0, 0, 0, 0, 0)), "John", "Robertson" },
                    { new Guid("26212ecc-c6f7-4ee6-9832-335e7619cd43"), new DateTimeOffset(new DateTime(1987, 3, 23, 2, 47, 20, 935, DateTimeKind.Unspecified).AddTicks(8509), new TimeSpan(0, 0, 0, 0, 0)), "Katie", "McDonald" }
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
