using System;
using System.Collections.Generic;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.Models;

namespace hfupilot.app.ViewModels
{
    class MeldungenViewModel : ObservableObject
    {
        private Meldung meldung;

        public MeldungenViewModel()
        {
            meldung = new Meldung();
        }

        public MeldungenViewModel(Meldung pmeldung)
        {
            meldung = new Meldung()
            {
                Id = pmeldung.Id,
                Art = pmeldung.Art,
                Datum = pmeldung.Datum,
                MeldungNachricht = pmeldung.MeldungNachricht,
                Zeit = pmeldung.Zeit
            };
        }

        #region Properties

        public int Id
        {
            get { return meldung.Id; }
            set
            {
                meldung.Id = value;
                RaisePropertyChanged();
            }
        }

        public  string Datum
        {
            get { return meldung.Datum; }
            set
            {
                meldung.Datum = value;
                RaisePropertyChanged();
            }
        }

        public string Zeit {
            get {return meldung.Zeit;}
            set
            {
                meldung.Zeit = value;
                RaisePropertyChanged();
            }
        }

        public string MeldungNachricht
        {
            get { return meldung.MeldungNachricht; }
            set
            {
                meldung.MeldungNachricht = value;
                RaisePropertyChanged();
            }
        }

        public int Art
        {
            get { return meldung.Art; }
            set
            {
                meldung.Art = value;
                RaisePropertyChanged();
            }
        }

        #endregion

    }
}
