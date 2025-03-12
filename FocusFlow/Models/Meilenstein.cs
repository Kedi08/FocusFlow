using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Meilenstein
    {
        [Key]
        public int MeilensteinId { get; set; }

        public string Name { get; set; }
        public DateTime? Datum { get; set; }

        // Ein Meilenstein gehört zu einem Projekt
        public int ProjektId { get; set; }
        public Projekt Projekt { get; set; }
    }
}
