using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PersonenDurchfuehrung
    {
        public int PersonenDurchfuehrungsId { get; set; }
        public int? PersonId { get; set; }
        public int? DurchfuehrungId { get; set; }
        public string Besuchsstatus { get; set; }
        public string Besuchsmodus { get; set; }
        public bool? IstDozent { get; set; }
        public bool? IstFachhoerer { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
