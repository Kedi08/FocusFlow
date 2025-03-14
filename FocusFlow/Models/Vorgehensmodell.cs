using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Vorgehensmodell
    {
        [Key]
        public int VorgehensmodellId { get; set; }

        public string Name { get; set; } = null!;

        public bool IstVorlage { get; set; }

        public ICollection<Projektphase> Projektphasen { get; set; } = new List<Projektphase>();
    }
}
