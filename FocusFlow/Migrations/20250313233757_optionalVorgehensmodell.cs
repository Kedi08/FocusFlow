using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class optionalVorgehensmodell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekte_Vorgehensmodelle_VorgehensmodellId",
                table: "Projekte");

            migrationBuilder.AlterColumn<int>(
                name: "VorgehensmodellId",
                table: "Projekte",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekte_Vorgehensmodelle_VorgehensmodellId",
                table: "Projekte",
                column: "VorgehensmodellId",
                principalTable: "Vorgehensmodelle",
                principalColumn: "VorgehensmodellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekte_Vorgehensmodelle_VorgehensmodellId",
                table: "Projekte");

            migrationBuilder.AlterColumn<int>(
                name: "VorgehensmodellId",
                table: "Projekte",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projekte_Vorgehensmodelle_VorgehensmodellId",
                table: "Projekte",
                column: "VorgehensmodellId",
                principalTable: "Vorgehensmodelle",
                principalColumn: "VorgehensmodellId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
