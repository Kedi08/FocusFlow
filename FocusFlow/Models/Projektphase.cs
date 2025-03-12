using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Projektphase
    {
        [Key]
        public int ProjektphaseId { get; set; }

        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }

        public DateTime? ReviewdatumGeplant { get; set; }
        public DateTime? ReviewdatumEffektiv { get; set; }
        public DateTime? Freigabedatum { get; set; }
        public string Freigabevermerk { get; set; }
        public string Status { get; set; }
        public float Fortschritt { get; set; }

        // Beziehung: Eine Projektphase gehört zu einem Projekt
        public int ProjektId { get; set; }
        public Projekt Projekt { get; set; }

        // Beziehung: Eine Projektphase kann mehrere Aktivitäten haben
        public ICollection<Aktivitaet> Aktivitaeten { get; set; }

        // Beziehung: Eine Projektphase kann mehrere Dokumente besitzen
        public ICollection<Dokument> Dokumente { get; set; }
    }
}
