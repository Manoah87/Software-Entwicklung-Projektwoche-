using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;

namespace hfupilot.app.ViewModels
{
    class DashboardTermineViewModel
    {

        #region Felder

        public ObservableCollection<TermineViewModel> Termine { get; set; }

        #endregion

        #region Konstruktor

        public DashboardTermineViewModel()
        {
            Termine = new ObservableCollection<TermineViewModel>();

            aktualisiereTermine();
        }

        #endregion

        private void aktualisiereTermine()
        {
            Termine.Clear();
            //Termine.Add()
        }




    }
}
