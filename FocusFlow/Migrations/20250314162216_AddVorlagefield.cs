using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddVorlagefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IstVorlage",
                table: "Vorgehensmodelle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Funktionen",
                table: "Mitarbeiter",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IstVorlage",
                table: "Vorgehensmodelle");

            migrationBuilder.DropColumn(
                name: "Funktionen",
                table: "Mitarbeiter");
        }
    }
}
