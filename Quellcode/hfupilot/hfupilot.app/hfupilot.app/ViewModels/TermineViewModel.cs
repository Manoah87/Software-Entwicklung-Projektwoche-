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
            termin = ptermine;
        }

        public int Id
        {
            get { return termin.Id; }
            set
            {
                termin.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Datum
        {
            get { return termin.Datum; }
            set
            {
                termin.Datum = value;
                RaisePropertyChanged("Datum");
            }
        }

        public string Zeit
        {
            get { return termin.Zeit; }
            set
            {
                termin.Zeit = value;
                RaisePropertyChanged("Zeit");
            }
        }

        public string Code
        {
            get { return termin.Code; }
            set
            {
                termin.Code = value;
                RaisePropertyChanged("Code");
            }
        }

        public string Titel
        {
            get { return termin.Titel; }
            set
            {
                termin.Titel = value;
                RaisePropertyChanged("Titel");
            }
        }

        public string Bezeichnung
        {
            get { return termin.Bezeichnung; }
            set
            {
                termin.Bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }

        public string Zimmer
        {
            get { return termin.Zimmer; }
            set
            {
                termin.Zimmer = value;
                RaisePropertyChanged("Zimmer");
            }
        }

        public string Lehrperson
        {
            get { return termin.Lehrperson; }
            set
            {
                termin.Lehrperson = value;
                RaisePropertyChanged("Lehrperson");
            }
        }

    }
}
