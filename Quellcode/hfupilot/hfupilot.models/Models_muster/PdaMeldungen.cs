using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PdaMeldungen
    {
        public int MeldungId { get; set; }
        public int? DatenplanId { get; set; }
        public string KontaktId { get; set; }
        public DateTime? DatumZeit { get; set; }
        public string Meldung { get; set; }
        public int? Art { get; set; }
        public int? Anzahl { get; set; }
        public DateTime? GemeldetAm { get; set; }
    }
}
