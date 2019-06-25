using System;
using System.Collections.Generic;

namespace hfupilot.models.Models
{
    public partial class ZParameter
    {
        public int ParameterId { get; set; }
        public string Code { get; set; }
        public string Bezeichnung { get; set; }
        public string Wert { get; set; }
        public int? ExtId { get; set; }
        public bool? IstAktiv { get; set; }
    }
}
