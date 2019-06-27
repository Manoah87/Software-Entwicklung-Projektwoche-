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

            Termine.Add(new TermineViewModel(){
                Id = 5,
                Datum = "heute",
                Zeit = "20:00",
                Code = "XYZ",
                Titel = "SW-Entwickeln",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Paul"
            });

            Termine.Add(new TermineViewModel()
            {
                Id = 5,
                Datum = "heute",
                Zeit = "20:00",
                Code = "XYZ",
                Titel = "SW-Entwickeln",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Heinz"
            });
            Termine.Add(new TermineViewModel()
            {
                Id = 5,
                Datum = "heute",
                Zeit = "18:00",
                Code = "asdf",
                Titel = "Datenbank",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Urs"
            });

        }




    }
}
