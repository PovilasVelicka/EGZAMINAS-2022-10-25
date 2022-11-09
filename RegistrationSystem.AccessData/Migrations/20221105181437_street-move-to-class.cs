using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    public partial class streetmovetoclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Cities",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetId",
                schema: "RegistrationSystem",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "CityName");
        }
    }
}
