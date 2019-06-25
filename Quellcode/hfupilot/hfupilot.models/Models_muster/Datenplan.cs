using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Datenplan
    {
        public int DatenplanId { get; set; }
        public int? DurchfuehrungId { get; set; }
        public DateTime? Datum { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? Ende { get; set; }
        public string Zimmer { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
