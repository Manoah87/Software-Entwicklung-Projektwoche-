using System;
using System.Collections.Generic;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Models;

namespace hfupilot.app.ViewModels
{
    class MeldungenViewModel : ObservableObject
    {
        private Meldung meldung;

        public MeldungenViewModel()
        {

            meldung = new Meldung();

        }

        #region Properties

        public int Id
        {
            get { return meldung.Id; }
            set
            {
                meldung.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public  string Datum
        {
            get { return meldung.Datum; }
            set
            {
                meldung.Datum = value;
                RaisePropertyChanged("Datum");
            }
        }

        public string Zeit {
            get {return meldung.Zeit;}
            set
            {
                meldung.Zeit = value;
                RaisePropertyChanged("Zeit");
            }
        }

        public string MeldungNachricht
        {
            get { return meldung.MeldungNachricht; }
            set
            {
                meldung.MeldungNachricht = value;
                RaisePropertyChanged("MeldungNachricht");
            }
        }

        public int Art
        {
            get { return meldung.Art; }
            set { meldung.Art = value; }
        }

        #endregion

    }
}
