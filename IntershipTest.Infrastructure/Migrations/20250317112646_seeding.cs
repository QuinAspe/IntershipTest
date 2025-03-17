using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IntershipTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Team A" },
                    { 2, "Team B" },
                    { 3, "Team C" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe", 1 },
                    { 2, new DateTime(1996, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith", 1 },
                    { 3, new DateTime(1994, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mike", "Johnson", 2 },
                    { 4, new DateTime(1993, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily", "Davis", 2 },
                    { 5, new DateTime(1997, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "James", "Williams", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
