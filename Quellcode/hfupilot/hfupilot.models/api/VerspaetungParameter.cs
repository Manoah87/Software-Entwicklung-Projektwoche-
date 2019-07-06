using System;
using System.Collections.Generic;
using System.Text;

namespace hfupilot.Models.api
{
    public class VerspaetungParameter
    {
        public int SessionID { get; set; }
        public int ID { get; set; }
        public int Anzal { get; set; }
        public string Grund { get; set; }
    }
}
