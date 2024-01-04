using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamowienia.Data.Migrations
{
    public partial class baza3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pracownik_id",
                table: "Zamowienia",
                newName: "pracownikId");

            migrationBuilder.RenameColumn(
                name: "data",
                table: "Zamowienia",
                newName: "dataZlozenia");

            migrationBuilder.AlterColumn<string>(
                name: "czyZrealizowano",
                table: "Zamowienia",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataRealizacji",
                table: "Zamowienia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TypUzytkownika",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nazwisko",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Imie",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataRealizacji",
                table: "Zamowienia");

            migrationBuilder.RenameColumn(
                name: "pracownikId",
                table: "Zamowienia",
                newName: "pracownik_id");

            migrationBuilder.RenameColumn(
                name: "dataZlozenia",
                table: "Zamowienia",
                newName: "data");

            migrationBuilder.AlterColumn<string>(
                name: "czyZrealizowano",
                table: "Zamowienia",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TypUzytkownika",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nazwisko",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Imie",
                table: "Pracownicy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
