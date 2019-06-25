using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Pruefungen
    {
        public int PruefungId { get; set; }
        public int? DatenplanId { get; set; }
        public decimal? MaxPunkte { get; set; }
        public string Bezeichnung { get; set; }
        public bool? IstMlz { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
