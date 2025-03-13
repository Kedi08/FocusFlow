using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Mitarbeiter
    {
        [Key]
        public int MitarbeiterId { get; set; }

        public string Personalnummer { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Vorname { get; set; } = string.Empty;
        public string Abteilung { get; set; } = string.Empty;
        public float Arbeitspensum { get; set; }
        [NotMapped]
        public List<string>? Funktionen { get; set; }
    }
}
