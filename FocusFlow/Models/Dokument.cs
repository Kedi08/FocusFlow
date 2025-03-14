using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Dokument
    {
        [Key]
        public int DokumentId { get; set; }

        public string Name { get; set; } = null!;
        public string Typ { get; set; } = null!;
        public string Pfad { get; set; } = null!;
        //[ForeignKey("Projekt")]
        //public int? ProjektId { get; set; }
        //[ForeignKey("Projektphase")]
        //public int? ProjektphaseId { get; set; }
        //[ForeignKey("Aktivitaet")]
        //public int? AktivitaetId { get; set; }

    }
}
