using Microsoft.EntityFrameworkCore;
using FocusFlow.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FocusFlow.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Vorgehensmodell> Vorgehensmodelle { get; set; }
        public DbSet<Projektphase> Projektphasen { get; set; }
        public DbSet<Aktivitaet> Aktivitaeten { get; set; }
        public DbSet<Meilenstein> Meilensteine { get; set; }
        public DbSet<Dokument> Dokumente { get; set; }
        public DbSet<PersonelleRessource> PersonelleRessourcen { get; set; }
        public DbSet<ExterneKosten> ExterneKosten { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Beispiel für Many-to-Many bei Mitarbeiter <--> Projekt:
            modelBuilder.Entity<Projekt>()
                .HasMany(p => p.Mitarbeiter)
                .WithMany(m => m.Projekte);

            base.OnModelCreating(modelBuilder);
        }
    }
}
