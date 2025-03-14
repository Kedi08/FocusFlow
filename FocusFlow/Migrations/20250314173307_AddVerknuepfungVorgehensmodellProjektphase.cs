using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddVerknuepfungVorgehensmodellProjektphase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projektphasen_Vorgehensmodelle_VorgehensmodellId",
                table: "Projektphasen");

            migrationBuilder.AlterColumn<int>(
                name: "VorgehensmodellId",
                table: "Projektphasen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projektphasen_Vorgehensmodelle_VorgehensmodellId",
                table: "Projektphasen",
                column: "VorgehensmodellId",
                principalTable: "Vorgehensmodelle",
                principalColumn: "VorgehensmodellId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projektphasen_Vorgehensmodelle_VorgehensmodellId",
                table: "Projektphasen");

            migrationBuilder.AlterColumn<int>(
                name: "VorgehensmodellId",
                table: "Projektphasen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projektphasen_Vorgehensmodelle_VorgehensmodellId",
                table: "Projektphasen",
                column: "VorgehensmodellId",
                principalTable: "Vorgehensmodelle",
                principalColumn: "VorgehensmodellId");
        }
    }
}
