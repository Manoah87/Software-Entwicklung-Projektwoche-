using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class LoginLogFile
    {
        public int LoginLogId { get; set; }
        public DateTime? Datum { get; set; }
        public string Benutzername { get; set; }
        public string BenutzerPw { get; set; }
        public int? Fehler { get; set; }
        public string Fehlermeldung { get; set; }
        public int? BenutzerId { get; set; }
        public string LetzteAktivitaet { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
