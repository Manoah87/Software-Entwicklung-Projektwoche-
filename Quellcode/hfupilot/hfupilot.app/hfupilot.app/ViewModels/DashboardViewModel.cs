using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    class DashboardViewModel : ObservableObject
    {
        private DashboardTermineViewModel _dashboardTermineViewModel;
        private DashboardMeldungenViewModel _dashboardMeldungenViewModel;


        public DashboardTermineViewModel DashboardTermineViewModel
        {
            get => _dashboardTermineViewModel;
            set
            {
                _dashboardTermineViewModel = value;
                RaisePropertyChanged();
            }

        }

        public DashboardMeldungenViewModel DashboardMeldungenViewModel
        {
            get => _dashboardMeldungenViewModel;
            set
            {
                _dashboardMeldungenViewModel = value;
                RaisePropertyChanged();
            }

        }

        public DashboardViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            DashboardMeldungenViewModel = new DashboardMeldungenViewModel(navigation,viewMapper,userContext,httpClient);
            DashboardTermineViewModel = new DashboardTermineViewModel(navigation,viewMapper,userContext,httpClient);

        }
    }
}

