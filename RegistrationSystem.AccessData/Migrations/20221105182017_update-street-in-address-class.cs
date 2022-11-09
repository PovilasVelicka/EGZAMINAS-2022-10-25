using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class updatestreetinaddressclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Street",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id");
        }
    }
}
