using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Personen
    {
        public int PersonId { get; set; }
        public string KontaktId { get; set; }
        public string Arbeitgeber { get; set; }
        public string Geschlecht { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string LandP { get; set; }
        public string Plzp { get; set; }
        public string StrasseNrP { get; set; }
        public string OrtP { get; set; }
        public string LandG { get; set; }
        public string Plzg { get; set; }
        public string StrasseNrG { get; set; }
        public string OrtG { get; set; }
        public string TelG { get; set; }
        public string TelP { get; set; }
        public string TelMobileP { get; set; }
        public string EmailP { get; set; }
        public string EmailG { get; set; }
        public string PersonStatus { get; set; }
        public string Stammklasse { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
