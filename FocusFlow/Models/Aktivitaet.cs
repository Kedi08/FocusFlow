using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Aktivitaet
    {
        [Key]
        public int AktivitaetId { get; set; }

        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }

        public float Budget { get; set; }
        public float KostenEffektiv { get; set; }
        public float Fortschritt { get; set; }

        // Verantwortliche Person (Mitarbeiter)
        public int? VerantwortlichePersonId { get; set; }
        public Mitarbeiter VerantwortlichePerson { get; set; }

        // Beziehung: Eine Aktivität gehört zu einer Projektphase
        public int ProjektphaseId { get; set; }
        public Projektphase Projektphase { get; set; }

        // Personelle Ressourcen (0..*)
        public ICollection<PersonelleRessource> Ressourcen { get; set; }

        // Externe Kosten (0..*)
        public ICollection<ExterneKosten> ExterneKosten { get; set; }

        // Dokumente (0..*)
        public ICollection<Dokument> Dokumente { get; set; }
    }
}
