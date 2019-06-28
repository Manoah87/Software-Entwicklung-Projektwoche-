using System;

namespace hfupilot.Models.api
{
    public class MeldungParameter
    {
        public int SessionID { get; set; }
        public int ID { get; set; }
        public string Meldung { get; set; }
        public DateTime AktiveBis { get; set; }
        public int Art { get; set; }

    }
}
