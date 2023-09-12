using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GuitarShop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CellCoordinates",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellCoordinates", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "CellCoordinates",
                columns: new[] { "ID", "color" },
                values: new object[,]
                {
                    { "11", "blue" },
                    { "12", "blue" },
                    { "13", "blue" },
                    { "21", "blue" },
                    { "22", "blue" },
                    { "23", "blue" },
                    { "31", "blue" },
                    { "32", "blue" },
                    { "33", "blue" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CellCoordinates");
        }
    }
}
