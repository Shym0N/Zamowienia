using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamowienia.Data.Migrations
{
    public partial class walidacyja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserName",
                table: "Zamowienia",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Zamowienia");
        }
    }
}
