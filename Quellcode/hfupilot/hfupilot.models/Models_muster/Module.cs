using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class Module
    {
        public int ModulId { get; set; }
        public string Code { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
