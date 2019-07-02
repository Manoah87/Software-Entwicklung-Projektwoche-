using hfupilot.app.CustomFramework.mvvm;
using hfupilot.Models;


namespace hfupilot.app.ViewModels
{
    class TermineViewModel : ObservableObject
    {
        private Termine termin;


        public TermineViewModel()
        {
            termin = new Termine();

        }

        public TermineViewModel(Termine ptermine)
        {
            termin = new Termine()
            {
                Id = ptermine.Id,
                Titel = ptermine.Titel,
                Bezeichnung = ptermine.Bezeichnung,
                Code = ptermine.Code,
                Datum = ptermine.Datum,
                Zeit = ptermine.Zeit,
                Zimmer = ptermine.Zimmer,
                Lehrperson = ptermine.Lehrperson
            };
        }

        public int Id
        {
            get { return termin.Id; }
            set
            {
                termin.Id = value;
                RaisePropertyChanged();
            }
        }

        public string Datum
        {
            get { return termin.Datum; }
            set
            {
                termin.Datum = value;
                RaisePropertyChanged();
            }
        }

        public string Zeit
        {
            get { return termin.Zeit; }
            set
            {
                termin.Zeit = value;
                RaisePropertyChanged();
            }
        }

        public string Code
        {
            get { return termin.Code; }
            set
            {
                termin.Code = value;
                RaisePropertyChanged();
            }
        }

        public string Titel
        {
            get { return termin.Titel; }
            set
            {
                termin.Titel = value;
                RaisePropertyChanged();
            }
        }

        public string Bezeichnung
        {
            get { return termin.Bezeichnung; }
            set
            {
                termin.Bezeichnung = value;
                RaisePropertyChanged();
            }
        }

        public string Zimmer
        {
            get { return termin.Zimmer; }
            set
            {
                termin.Zimmer = value;
                RaisePropertyChanged();
            }
        }

        public string Lehrperson
        {
            get
            {
                return termin.Lehrperson;
            }
            set
            {
                termin.Lehrperson = value;
                RaisePropertyChanged();
            }
        }

    }
}
