using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Dokument
    {
        [Key]
        public int DokumentId { get; set; }

        public string Name { get; set; }
        public string Typ { get; set; }
        public string Pfad { get; set; }

        // Optional: Dokument kann zu Projekt, Projektphase und/oder Aktivität gehören
        public int? ProjektId { get; set; }
        public Projekt Projekt { get; set; }

        public int? ProjektphaseId { get; set; }
        public Projektphase Projektphase { get; set; }

        public int? AktivitaetId { get; set; }
        public Aktivitaet Aktivitaet { get; set; }
    }
}
