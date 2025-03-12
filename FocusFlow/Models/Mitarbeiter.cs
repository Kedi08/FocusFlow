using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Mitarbeiter
    {
        [Key]
        public int MitarbeiterId { get; set; }

        public string Personalnummer { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Abteilung { get; set; }
        public float Arbeitspensum { get; set; }

        // Rollen ggf. als JSON oder Komma-getrennte Liste
        public string Rollen { get; set; }

        // Many-to-Many: Ein Mitarbeiter kann in mehreren Projekten sein
        public ICollection<Projekt> Projekte { get; set; }
    }
}
