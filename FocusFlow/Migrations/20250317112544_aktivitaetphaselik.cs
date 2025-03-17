using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class aktivitaetphaselik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aktivitaeten_Projektphasen_ProjektphaseId",
                table: "Aktivitaeten");

            migrationBuilder.AlterColumn<int>(
                name: "ProjektphaseId",
                table: "Aktivitaeten",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aktivitaeten_Projektphasen_ProjektphaseId",
                table: "Aktivitaeten",
                column: "ProjektphaseId",
                principalTable: "Projektphasen",
                principalColumn: "ProjektphaseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aktivitaeten_Projektphasen_ProjektphaseId",
                table: "Aktivitaeten");

            migrationBuilder.AlterColumn<int>(
                name: "ProjektphaseId",
                table: "Aktivitaeten",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aktivitaeten_Projektphasen_ProjektphaseId",
                table: "Aktivitaeten",
                column: "ProjektphaseId",
                principalTable: "Projektphasen",
                principalColumn: "ProjektphaseId");
        }
    }
}
