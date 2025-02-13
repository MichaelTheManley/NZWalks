using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("478172f7-0576-4abd-ac66-32f73c3058d7"), "Easy" },
                    { new Guid("8e59e18c-1c88-4dca-bfd4-77933315a677"), "Medium" },
                    { new Guid("97ee2524-e280-4e7b-803a-28b56820a2d8"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("01c9f4e7-8fde-4c3c-8a8a-bf6adadde383"), "UK", "England", "london.image.co.za" },
                    { new Guid("3ed29439-e89f-428c-a650-45669c12d200"), "NY", "New York", "nyork.image.co.za" },
                    { new Guid("3f840971-4f12-47cd-9dbd-e01c003f5e2b"), "AUS", "Australia", "aus.image.co.za" },
                    { new Guid("7b5df87d-0264-4ddf-9cc3-30e88fdb380d"), "AKL", "Auckland", "auck.image.co.za" },
                    { new Guid("9c4f96f2-ae51-43f4-a63a-b516026b1068"), "RSA", "South Africa", "rsa.image.co.za" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("478172f7-0576-4abd-ac66-32f73c3058d7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8e59e18c-1c88-4dca-bfd4-77933315a677"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("97ee2524-e280-4e7b-803a-28b56820a2d8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("01c9f4e7-8fde-4c3c-8a8a-bf6adadde383"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3ed29439-e89f-428c-a650-45669c12d200"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3f840971-4f12-47cd-9dbd-e01c003f5e2b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7b5df87d-0264-4ddf-9cc3-30e88fdb380d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9c4f96f2-ae51-43f4-a63a-b516026b1068"));
        }
    }
}
