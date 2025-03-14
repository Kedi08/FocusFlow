using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddReihenvolgefuerProjektphase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefinierteZeitspanne",
                table: "Projektphasen");

            migrationBuilder.AddColumn<int>(
                name: "DauerInTagen",
                table: "Projektphasen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Reihenfolge",
                table: "Projektphasen",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DauerInTagen",
                table: "Projektphasen");

            migrationBuilder.DropColumn(
                name: "Reihenfolge",
                table: "Projektphasen");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DefinierteZeitspanne",
                table: "Projektphasen",
                type: "time",
                nullable: true);
        }
    }
}
