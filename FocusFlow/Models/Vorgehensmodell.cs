using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Vorgehensmodell
    {
        [Key]
        public int VorgehensmodellId { get; set; }

        public string Name { get; set; }

        // Ein Vorgehensmodell kann mehrere Projektphasen definieren
        public ICollection<Projektphase> Phasen { get; set; }
    }
}
