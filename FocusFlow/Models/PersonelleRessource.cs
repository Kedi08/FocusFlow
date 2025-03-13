using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class PersonelleRessource
    {
        [Key]
        public int PersonelleRessourceId { get; set; }

        public string Funktion { get; set; }
        public float ZeitBudget { get; set; }
        public float? ZeitEffektiv { get; set; }
        public string? Abweichungsbegruendung { get; set; }
    }
}
