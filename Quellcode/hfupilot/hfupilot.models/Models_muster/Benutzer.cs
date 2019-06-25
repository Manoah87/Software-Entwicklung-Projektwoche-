using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Benutzer
    {
        public int BenutzerId { get; set; }
        public int? PersonId { get; set; }
        public string Passwort { get; set; }
        public string Anmeldename { get; set; }
        public int? Anmeldeversuche { get; set; }
        public int? Fehler { get; set; }
        public string Fehlermeldung { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
