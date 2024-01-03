using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamowienia.Data.Migrations
{
    public partial class zamowienia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    listaPrzedmiotow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pracownik_id = table.Column<int>(type: "int", nullable: false),
                    czyZrealizowano = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zamowienia");
        }
    }
}
