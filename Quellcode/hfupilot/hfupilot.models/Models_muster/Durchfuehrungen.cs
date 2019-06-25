using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Durchfuehrungen
    {
        public int DurchfuehrungId { get; set; }
        public int? ModulId { get; set; }
        public decimal? AnzahlLektionen { get; set; }
        public int? AnzahlTeilnehmer { get; set; }
        public DateTime? DatumStart { get; set; }
        public DateTime? DatumEnde { get; set; }
        public string Titel { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
