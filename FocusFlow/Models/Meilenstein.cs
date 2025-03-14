﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.Models
{
    public class Meilenstein
    {
        [Key]
        public int MeilensteinId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Datum { get; set; }
    }
}
