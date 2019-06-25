using System;
using System.Collections.Generic;
using System.Text;

namespace hfupilot.models
{
    public class Stundenplan
    {
        public int FehlerMeldung { get; set; }
        public string MyProperty { get; set; }
        public int ID { get; set; }
        public string Datum { get; set; }
        public string Zeit { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Bezeichnung { get; set; }
        public string Zimmer { get; set; }
        public string Lehrperson { get; set; }
    }
}
