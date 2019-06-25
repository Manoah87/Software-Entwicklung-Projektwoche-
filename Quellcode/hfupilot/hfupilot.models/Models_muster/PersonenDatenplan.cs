using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PersonenDatenplan
    {
        public int PersonDatenplanId { get; set; }
        public int? PersonId { get; set; }
        public int? DatenplanId { get; set; }
        public int? AnwesenheitStatus { get; set; }
        public decimal? IstVerspaetetUm { get; set; }
        public string Kommentar { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
