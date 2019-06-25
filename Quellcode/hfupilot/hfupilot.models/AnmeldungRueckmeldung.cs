namespace hfupilot.models
{
    public class AnmeldungRueckmeldung
    {
        public int Fehler { get; set; }
        public string Fehlermeldung { get; set; }
        public int Session { get; set; }
        public int Stufe { get; set; }
    }
}
