using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Aktivitaet
    {
        [Key]
        public int AktivitaetId { get; set; }
        public string Name { get; set; } = null!;

        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }

        public float? Budget { get; set; }
        public float? KostenEffektiv { get; set; }
        public float? Fortschritt { get; set; }

        [ForeignKey("Projektphase")]
        public int ProjektphaseId { get; set; }
        public virtual Projektphase? Projektphase { get; set; }

        // Verantwortlicher Mitarbeiter
        [ForeignKey("Mitarbeiter")]
        public int MitarbeiterId { get; set; }
        public virtual Mitarbeiter? Mitarbeiter { get; set; }

        public ICollection<PersonelleRessource>? Ressourcen { get; set; }
        public ICollection<ExterneKosten>? ExterneKosten { get; set; }
        public ICollection<Dokument>? Dokumente { get; set; }
    }
}
