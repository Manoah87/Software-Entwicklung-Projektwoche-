namespace hfupilot.Models
{
    public class Meldung : BasisFehlerProperties
    {
        public int Id { get; set; }
        public string Datum { get; set; }
        public string Zeit { get; set; }
        public string MeldungNachricht { get; set; }
        public int Art { get; set; }

    }
}
