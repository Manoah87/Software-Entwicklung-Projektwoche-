using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PdaSessions
    {
        public int SessionId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? DatumLogin { get; set; }
        public DateTime? DatumLetzteAktivitaet { get; set; }
    }
}
