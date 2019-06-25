using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PersonenPruefung
    {
        public int PersonPruefungId { get; set; }
        public int? PersonId { get; set; }
        public int? PruefungId { get; set; }
        public decimal? Punkte { get; set; }
        public decimal? Note { get; set; }
        public bool? IstNoteManuell { get; set; }
        public string Kommentar { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
