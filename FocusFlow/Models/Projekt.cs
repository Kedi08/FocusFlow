using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Projekt
    {
        [Key]
        public int ProjektId { get; set; }
        public string Titel { get; set; } = null!;
        public string Beschreibung { get; set; } = null!;
        public DateTime? Bewilligungsdatum { get; set; }
        public string? Prioritaet { get; set; }
        public string? Status { get; set; }

        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }
        public float? Fortschritt { get; set; }

        [ForeignKey("Mitarbeiter")]
        public int ProjektleiterId { get; set; }
        public virtual Mitarbeiter? Projektleiter { get; set; }
        public  Vorgehensmodell? Vorgehensmodell { get; set; }
        public ICollection<Dokument>? Dokumente { get; set; }
    }
}
