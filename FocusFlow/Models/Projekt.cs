using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FocusFlow.Models
{
    public class Projekt
    {
        [Key]
        public int ProjektId { get; set; }

        public string Projektreferenz { get; set; }
        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public DateTime? Bewilligungsdatum { get; set; }
        public string Prioritaet { get; set; }
        public string Status { get; set; }

        public DateTime? StartdatumGeplant { get; set; }
        public DateTime? EnddatumGeplant { get; set; }
        public DateTime? StartdatumEffektiv { get; set; }
        public DateTime? EnddatumEffektiv { get; set; }
        public float Fortschritt { get; set; }

        // Beziehung: Ein Projekt hat genau ein Vorgehensmodell
        public int? VorgehensmodellId { get; set; }
        public Vorgehensmodell Vorgehensmodell { get; set; }

        // Beziehung: Ein Projekt kann viele Dokumente haben
        public ICollection<Dokument> Dokumente { get; set; }

        // Beziehung: Ein Projekt kann viele Mitarbeiter haben (Many-to-Many)
        public ICollection<Mitarbeiter> Mitarbeiter { get; set; }

        // Beziehung: Ein Projekt kann viele Projektphasen haben
        public ICollection<Projektphase> Projektphasen { get; set; }

        // Beziehung: Ein Projekt kann mehrere Meilensteine haben (optional)
        public ICollection<Meilenstein> Meilensteine { get; set; }
    }
}
