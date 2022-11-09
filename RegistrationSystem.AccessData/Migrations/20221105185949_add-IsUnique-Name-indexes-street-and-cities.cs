using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class addIsUniqueNameindexesstreetandcities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Streets",
                newName: "Streets",
                newSchema: "RegistrationSystem");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "Cities",
                newSchema: "RegistrationSystem");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Streets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "RegistrationSystem",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "UI_StreetName",
                schema: "RegistrationSystem",
                table: "Streets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UI_CityName",
                schema: "RegistrationSystem",
                table: "Cities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_StreetName",
                schema: "RegistrationSystem",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "UI_CityName",
                schema: "RegistrationSystem",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "Streets",
                schema: "RegistrationSystem",
                newName: "Streets");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "RegistrationSystem",
                newName: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
