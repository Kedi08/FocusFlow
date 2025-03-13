﻿// <auto-generated />
using System;
using FocusFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FocusFlow.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250313171434_removeMitarbeiterIds")]
    partial class removeMitarbeiterIds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FocusFlow.Models.Aktivitaet", b =>
                {
                    b.Property<int>("AktivitaetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AktivitaetId"));

                    b.Property<float?>("Budget")
                        .HasColumnType("real");

                    b.Property<DateTime?>("EnddatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EnddatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Fortschritt")
                        .HasColumnType("real");

                    b.Property<float?>("KostenEffektiv")
                        .HasColumnType("real");

                    b.Property<int>("MitarbeiterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjektphaseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartdatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartdatumGeplant")
                        .HasColumnType("datetime2");

                    b.HasKey("AktivitaetId");

                    b.HasIndex("MitarbeiterId");

                    b.HasIndex("ProjektphaseId");

                    b.ToTable("Aktivitaeten");
                });

            modelBuilder.Entity("FocusFlow.Models.Dokument", b =>
                {
                    b.Property<int>("DokumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DokumentId"));

                    b.Property<int?>("AktivitaetId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pfad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjektId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjektphaseId")
                        .HasColumnType("int");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DokumentId");

                    b.HasIndex("AktivitaetId");

                    b.HasIndex("ProjektId");

                    b.HasIndex("ProjektphaseId");

                    b.ToTable("Dokumente");
                });

            modelBuilder.Entity("FocusFlow.Models.ExterneKosten", b =>
                {
                    b.Property<int>("ExterneKostenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExterneKostenId"));

                    b.Property<string>("Abweichungsbegruendung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AktivitaetId")
                        .HasColumnType("int");

                    b.Property<float>("Budget")
                        .HasColumnType("real");

                    b.Property<float?>("KostenEffektiv")
                        .HasColumnType("real");

                    b.Property<string>("Kostenart")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExterneKostenId");

                    b.HasIndex("AktivitaetId");

                    b.ToTable("ExterneKosten");
                });

            modelBuilder.Entity("FocusFlow.Models.Meilenstein", b =>
                {
                    b.Property<int>("MeilensteinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeilensteinId"));

                    b.Property<DateTime?>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjektphaseId")
                        .HasColumnType("int");

                    b.HasKey("MeilensteinId");

                    b.HasIndex("ProjektphaseId");

                    b.ToTable("Meilensteine");
                });

            modelBuilder.Entity("FocusFlow.Models.Mitarbeiter", b =>
                {
                    b.Property<int>("MitarbeiterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MitarbeiterId"));

                    b.Property<string>("Abteilung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Arbeitspensum")
                        .HasColumnType("real");

                    b.Property<string>("Funktion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Personalnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MitarbeiterId");

                    b.ToTable("Mitarbeiter");
                });

            modelBuilder.Entity("FocusFlow.Models.PersonelleRessource", b =>
                {
                    b.Property<int>("PersonelleRessourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonelleRessourceId"));

                    b.Property<string>("Abweichungsbegruendung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AktivitaetId")
                        .HasColumnType("int");

                    b.Property<string>("Funktion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ZeitBudget")
                        .HasColumnType("real");

                    b.Property<float?>("ZeitEffektiv")
                        .HasColumnType("real");

                    b.HasKey("PersonelleRessourceId");

                    b.HasIndex("AktivitaetId");

                    b.ToTable("PersonelleRessourcen");
                });

            modelBuilder.Entity("FocusFlow.Models.Projekt", b =>
                {
                    b.Property<int>("ProjektId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjektId"));

                    b.Property<string>("Beschreibung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Bewilligungsdatum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EnddatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EnddatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Fortschritt")
                        .HasColumnType("real");

                    b.Property<string>("Prioritaet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartdatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartdatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VorgehensmodellId")
                        .HasColumnType("int");

                    b.HasKey("ProjektId");

                    b.HasIndex("VorgehensmodellId");

                    b.ToTable("Projekte");
                });

            modelBuilder.Entity("FocusFlow.Models.Projektphase", b =>
                {
                    b.Property<int>("ProjektphaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjektphaseId"));

                    b.Property<TimeSpan?>("DefinierteZeitspanne")
                        .HasColumnType("time");

                    b.Property<DateTime?>("EnddatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EnddatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Fortschritt")
                        .HasColumnType("real");

                    b.Property<DateTime?>("Freigabedatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Freigabevermerk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjektId")
                        .HasColumnType("int");

                    b.Property<string>("ProjektphaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReviewdatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReviewdatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartdatumEffektiv")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartdatumGeplant")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VorgehensmodellId")
                        .HasColumnType("int");

                    b.HasKey("ProjektphaseId");

                    b.HasIndex("ProjektId");

                    b.HasIndex("VorgehensmodellId");

                    b.ToTable("Projektphasen");
                });

            modelBuilder.Entity("FocusFlow.Models.Vorgehensmodell", b =>
                {
                    b.Property<int>("VorgehensmodellId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VorgehensmodellId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VorgehensmodellId");

                    b.ToTable("Vorgehensmodelle");
                });

            modelBuilder.Entity("MitarbeiterProjekt", b =>
                {
                    b.Property<int>("MitarbeiterId")
                        .HasColumnType("int");

                    b.Property<int>("ProjekteProjektId")
                        .HasColumnType("int");

                    b.HasKey("MitarbeiterId", "ProjekteProjektId");

                    b.HasIndex("ProjekteProjektId");

                    b.ToTable("MitarbeiterProjekt");
                });

            modelBuilder.Entity("FocusFlow.Models.Aktivitaet", b =>
                {
                    b.HasOne("FocusFlow.Models.Mitarbeiter", "Mitarbeiter")
                        .WithMany()
                        .HasForeignKey("MitarbeiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FocusFlow.Models.Projektphase", "Projektphase")
                        .WithMany("Aktivitaeten")
                        .HasForeignKey("ProjektphaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mitarbeiter");

                    b.Navigation("Projektphase");
                });

            modelBuilder.Entity("FocusFlow.Models.Dokument", b =>
                {
                    b.HasOne("FocusFlow.Models.Aktivitaet", null)
                        .WithMany("Dokumente")
                        .HasForeignKey("AktivitaetId");

                    b.HasOne("FocusFlow.Models.Projekt", null)
                        .WithMany("Dokumente")
                        .HasForeignKey("ProjektId");

                    b.HasOne("FocusFlow.Models.Projektphase", null)
                        .WithMany("Meilensteine")
                        .HasForeignKey("ProjektphaseId");
                });

            modelBuilder.Entity("FocusFlow.Models.ExterneKosten", b =>
                {
                    b.HasOne("FocusFlow.Models.Aktivitaet", null)
                        .WithMany("ExterneKosten")
                        .HasForeignKey("AktivitaetId");
                });

            modelBuilder.Entity("FocusFlow.Models.Meilenstein", b =>
                {
                    b.HasOne("FocusFlow.Models.Projektphase", "Projektphase")
                        .WithMany()
                        .HasForeignKey("ProjektphaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projektphase");
                });

            modelBuilder.Entity("FocusFlow.Models.PersonelleRessource", b =>
                {
                    b.HasOne("FocusFlow.Models.Aktivitaet", null)
                        .WithMany("Ressourcen")
                        .HasForeignKey("AktivitaetId");
                });

            modelBuilder.Entity("FocusFlow.Models.Projekt", b =>
                {
                    b.HasOne("FocusFlow.Models.Vorgehensmodell", "Vorgehensmodell")
                        .WithMany()
                        .HasForeignKey("VorgehensmodellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vorgehensmodell");
                });

            modelBuilder.Entity("FocusFlow.Models.Projektphase", b =>
                {
                    b.HasOne("FocusFlow.Models.Projekt", "Projekt")
                        .WithMany("Projektphasen")
                        .HasForeignKey("ProjektId");

                    b.HasOne("FocusFlow.Models.Vorgehensmodell", "Vorgehensmodell")
                        .WithMany("Projektphasen")
                        .HasForeignKey("VorgehensmodellId");

                    b.Navigation("Projekt");

                    b.Navigation("Vorgehensmodell");
                });

            modelBuilder.Entity("MitarbeiterProjekt", b =>
                {
                    b.HasOne("FocusFlow.Models.Mitarbeiter", null)
                        .WithMany()
                        .HasForeignKey("MitarbeiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FocusFlow.Models.Projekt", null)
                        .WithMany()
                        .HasForeignKey("ProjekteProjektId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FocusFlow.Models.Aktivitaet", b =>
                {
                    b.Navigation("Dokumente");

                    b.Navigation("ExterneKosten");

                    b.Navigation("Ressourcen");
                });

            modelBuilder.Entity("FocusFlow.Models.Projekt", b =>
                {
                    b.Navigation("Dokumente");

                    b.Navigation("Projektphasen");
                });

            modelBuilder.Entity("FocusFlow.Models.Projektphase", b =>
                {
                    b.Navigation("Aktivitaeten");

                    b.Navigation("Meilensteine");
                });

            modelBuilder.Entity("FocusFlow.Models.Vorgehensmodell", b =>
                {
                    b.Navigation("Projektphasen");
                });
#pragma warning restore 612, 618
        }
    }
}
