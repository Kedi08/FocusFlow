using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class ExterneKosten
    {
        [Key]
        public int ExterneKostenId { get; set; }

        public string? Kostenart { get; set; }
        public float Budget { get; set; }
        public float? KostenEffektiv { get; set; }
        public string? Abweichungsbegruendung { get; set; }
    }
}
