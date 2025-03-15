using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Projektphase
    {
        [Key]
        public int ProjektphaseId { get; set; }
        public string ProjektphaseName { get; set; } = null!;
        public int DauerInTagen { get; set; }
        public double Reihenfolge { get; set; }
        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }

        public DateTime? ReviewdatumGeplant { get; set; }
        public DateTime? ReviewdatumEffektiv { get; set; }
        public DateTime? Freigabedatum { get; set; }
        public string? Freigabevermerk { get; set; }
        public string? Status { get; set; }
        public float? Fortschritt { get; set; }
        public ICollection<Aktivitaet>? Aktivitaeten { get; set; }
        public ICollection<Meilenstein>? Meilensteine { get; set; }
        public ICollection<Dokument>? Dokumente { get; set; }

        [ForeignKey("Vorgehensmodell")]
        public int VorgehensmodellId { get; set; }
        public Vorgehensmodell? Vorgehensmodell { get; set; }
    }
}
