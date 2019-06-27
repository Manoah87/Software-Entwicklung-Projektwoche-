using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;



namespace hfupilot.app.ViewModels
{
    class DashboardMeldungenViewModel
    {
        #region Felder

        public ObservableCollection<MeldungenViewModel> Meldungen { get; set; }

        #endregion


        public DashboardMeldungenViewModel()
        {
            Meldungen = new ObservableCollection<MeldungenViewModel>();

            aktualisiereMeldungen();
        }

        private void aktualisiereMeldungen()
        {
         Meldungen.Add(new MeldungenViewModel() {Art = 1,Datum = "26.07.17",Id=5,MeldungNachricht = "Hello World",Zeit = "20:00"});
         Meldungen.Add(new MeldungenViewModel() {Art = 2,Datum = "29.07.17",Id=1,MeldungNachricht = "Tschau World",Zeit = "22:00"});
         Meldungen.Add(new MeldungenViewModel() {Art = 3,Datum = "30.07.17",Id=6,MeldungNachricht = "Verschiebung",Zeit = "05:00"});

        }
    }
}
