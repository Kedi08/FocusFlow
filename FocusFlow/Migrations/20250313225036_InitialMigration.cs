using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mitarbeiter",
                columns: table => new
                {
                    MitarbeiterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Personalnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abteilung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arbeitspensum = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitarbeiter", x => x.MitarbeiterId);
                });

            migrationBuilder.CreateTable(
                name: "Vorgehensmodelle",
                columns: table => new
                {
                    VorgehensmodellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorgehensmodelle", x => x.VorgehensmodellId);
                });

            migrationBuilder.CreateTable(
                name: "Projekte",
                columns: table => new
                {
                    ProjektId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bewilligungsdatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prioritaet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartdatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartdatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fortschritt = table.Column<float>(type: "real", nullable: true),
                    ProjektleiterId = table.Column<int>(type: "int", nullable: false),
                    VorgehensmodellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekte", x => x.ProjektId);
                    table.ForeignKey(
                        name: "FK_Projekte_Mitarbeiter_ProjektleiterId",
                        column: x => x.ProjektleiterId,
                        principalTable: "Mitarbeiter",
                        principalColumn: "MitarbeiterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projekte_Vorgehensmodelle_VorgehensmodellId",
                        column: x => x.VorgehensmodellId,
                        principalTable: "Vorgehensmodelle",
                        principalColumn: "VorgehensmodellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projektphasen",
                columns: table => new
                {
                    ProjektphaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjektphaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefinierteZeitspanne = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartdatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartdatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewdatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewdatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Freigabedatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Freigabevermerk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fortschritt = table.Column<float>(type: "real", nullable: true),
                    VorgehensmodellId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projektphasen", x => x.ProjektphaseId);
                    table.ForeignKey(
                        name: "FK_Projektphasen_Vorgehensmodelle_VorgehensmodellId",
                        column: x => x.VorgehensmodellId,
                        principalTable: "Vorgehensmodelle",
                        principalColumn: "VorgehensmodellId");
                });

            migrationBuilder.CreateTable(
                name: "Aktivitaeten",
                columns: table => new
                {
                    AktivitaetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartdatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumGeplant = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartdatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnddatumEffektiv = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Budget = table.Column<float>(type: "real", nullable: true),
                    KostenEffektiv = table.Column<float>(type: "real", nullable: true),
                    Fortschritt = table.Column<float>(type: "real", nullable: true),
                    MitarbeiterId = table.Column<int>(type: "int", nullable: false),
                    ProjektphaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktivitaeten", x => x.AktivitaetId);
                    table.ForeignKey(
                        name: "FK_Aktivitaeten_Mitarbeiter_MitarbeiterId",
                        column: x => x.MitarbeiterId,
                        principalTable: "Mitarbeiter",
                        principalColumn: "MitarbeiterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aktivitaeten_Projektphasen_ProjektphaseId",
                        column: x => x.ProjektphaseId,
                        principalTable: "Projektphasen",
                        principalColumn: "ProjektphaseId");
                });

            migrationBuilder.CreateTable(
                name: "Meilensteine",
                columns: table => new
                {
                    MeilensteinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjektphaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meilensteine", x => x.MeilensteinId);
                    table.ForeignKey(
                        name: "FK_Meilensteine_Projektphasen_ProjektphaseId",
                        column: x => x.ProjektphaseId,
                        principalTable: "Projektphasen",
                        principalColumn: "ProjektphaseId");
                });

            migrationBuilder.CreateTable(
                name: "Dokumente",
                columns: table => new
                {
                    DokumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pfad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktivitaetId = table.Column<int>(type: "int", nullable: true),
                    ProjektId = table.Column<int>(type: "int", nullable: true),
                    ProjektphaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumente", x => x.DokumentId);
                    table.ForeignKey(
                        name: "FK_Dokumente_Aktivitaeten_AktivitaetId",
                        column: x => x.AktivitaetId,
                        principalTable: "Aktivitaeten",
                        principalColumn: "AktivitaetId");
                    table.ForeignKey(
                        name: "FK_Dokumente_Projekte_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekte",
                        principalColumn: "ProjektId");
                    table.ForeignKey(
                        name: "FK_Dokumente_Projektphasen_ProjektphaseId",
                        column: x => x.ProjektphaseId,
                        principalTable: "Projektphasen",
                        principalColumn: "ProjektphaseId");
                });

            migrationBuilder.CreateTable(
                name: "ExterneKosten",
                columns: table => new
                {
                    ExterneKostenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kostenart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<float>(type: "real", nullable: false),
                    KostenEffektiv = table.Column<float>(type: "real", nullable: true),
                    Abweichungsbegruendung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktivitaetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExterneKosten", x => x.ExterneKostenId);
                    table.ForeignKey(
                        name: "FK_ExterneKosten_Aktivitaeten_AktivitaetId",
                        column: x => x.AktivitaetId,
                        principalTable: "Aktivitaeten",
                        principalColumn: "AktivitaetId");
                });

            migrationBuilder.CreateTable(
                name: "PersonelleRessourcen",
                columns: table => new
                {
                    PersonelleRessourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Funktion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZeitBudget = table.Column<float>(type: "real", nullable: false),
                    ZeitEffektiv = table.Column<float>(type: "real", nullable: true),
                    Abweichungsbegruendung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktivitaetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelleRessourcen", x => x.PersonelleRessourceId);
                    table.ForeignKey(
                        name: "FK_PersonelleRessourcen_Aktivitaeten_AktivitaetId",
                        column: x => x.AktivitaetId,
                        principalTable: "Aktivitaeten",
                        principalColumn: "AktivitaetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aktivitaeten_MitarbeiterId",
                table: "Aktivitaeten",
                column: "MitarbeiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Aktivitaeten_ProjektphaseId",
                table: "Aktivitaeten",
                column: "ProjektphaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_AktivitaetId",
                table: "Dokumente",
                column: "AktivitaetId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_ProjektId",
                table: "Dokumente",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumente_ProjektphaseId",
                table: "Dokumente",
                column: "ProjektphaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExterneKosten_AktivitaetId",
                table: "ExterneKosten",
                column: "AktivitaetId");

            migrationBuilder.CreateIndex(
                name: "IX_Meilensteine_ProjektphaseId",
                table: "Meilensteine",
                column: "ProjektphaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelleRessourcen_AktivitaetId",
                table: "PersonelleRessourcen",
                column: "AktivitaetId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekte_ProjektleiterId",
                table: "Projekte",
                column: "ProjektleiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekte_VorgehensmodellId",
                table: "Projekte",
                column: "VorgehensmodellId");

            migrationBuilder.CreateIndex(
                name: "IX_Projektphasen_VorgehensmodellId",
                table: "Projektphasen",
                column: "VorgehensmodellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumente");

            migrationBuilder.DropTable(
                name: "ExterneKosten");

            migrationBuilder.DropTable(
                name: "Meilensteine");

            migrationBuilder.DropTable(
                name: "PersonelleRessourcen");

            migrationBuilder.DropTable(
                name: "Projekte");

            migrationBuilder.DropTable(
                name: "Aktivitaeten");

            migrationBuilder.DropTable(
                name: "Mitarbeiter");

            migrationBuilder.DropTable(
                name: "Projektphasen");

            migrationBuilder.DropTable(
                name: "Vorgehensmodelle");
        }
    }
}
