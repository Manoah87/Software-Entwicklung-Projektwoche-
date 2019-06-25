using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class PdaPersonen
    {
        public int PersonId { get; set; }
        public string KontaktId { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public int? Stufe { get; set; }
        public string Stammklasse { get; set; }
        public string Benutzer { get; set; }
        public string Passwort { get; set; }
    }
}
