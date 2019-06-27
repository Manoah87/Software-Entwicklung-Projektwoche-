namespace hfupilot.app.Models
{
    public class Termine : BasisFehlerProperties
    {
        public int Id { get; set; }
        public string Datum { get; set; }
        public string Zeit { get; set; }
        public string Code { get; set; }
        public string Titel { get; set; }
        public string Bezeichnung { get; set; }
        public string Zimmer { get; set; }
        public string Lehrperson { get; set; }
    }
}
