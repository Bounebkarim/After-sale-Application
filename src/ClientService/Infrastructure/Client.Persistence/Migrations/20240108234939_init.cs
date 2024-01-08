using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Client.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Cin", "CreatedBy", "DateCreated", "DateModified", "LastName", "ModifiedBy", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("377a02b7-4b2e-3b69-ac46-530e8054a026"), "262-3388 A, Road", "32714860", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2261), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2262), "West", "emna", "Quinlan", "14563616" },
                    { new Guid("55122af3-bb25-a27c-5751-bacec0a31e67"), "Ap #566-3797 Non Rd.", "77643679", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2253), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2254), "Brennan", "emna", "Zane", "28362418" },
                    { new Guid("82941a0d-d4e7-4c43-88b0-f48897bdb94c"), "Rue de tunis km 0.5 cité jardin", "11020305", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2229), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2231), "Ben Zina", "emna", "Emna", "25756063" },
                    { new Guid("9273f710-21d6-0cba-42b8-7a22a3338ca9"), "157-1645 Dictum. St.", "12248084", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2251), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2251), "Boone", "emna", "Imelda", "31728272" },
                    { new Guid("a21d744a-c328-86d7-d45f-d3a483cf1bca"), "123 Main Street", "78901234", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2265), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2266), "Crawford", "emna", "Lucas", "45678901" },
                    { new Guid("b21d744a-c328-86d7-d45f-d3a483cf1bca"), "P.O. Box 262, 1469 Hendrerit Road", "24122383", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2248), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2249), "Rowland", "emna", "Raven", "37724065" },
                    { new Guid("b383ee1f-9073-e756-c759-894f9bef1d39"), "Ap #467-720 Cursus Road", "87646134", "karim", new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2256), new DateTime(2024, 1, 8, 23, 49, 39, 169, DateTimeKind.Utc).AddTicks(2257), "Sullivan", "emna", "Malcolm", "98258532" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
