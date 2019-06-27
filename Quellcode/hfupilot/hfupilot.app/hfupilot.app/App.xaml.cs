using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Services;
using hfupilot.app.ViewModels;
using hfupilot.app.Views;
using SimpleInjector;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace hfupilot.app
{
    public partial class App : Application
    {
        public static Container Services { get; } = new Container();
        
        public App()
        {
            InitializeComponent();

            //TODO prüfen ob abgeschlossen (sealed)
            if (!DesignMode.IsDesignModeEnabled)
            {
                //var mainPage = new AnmeldenView();
                var mainPage = new DashboardView();
                var navigationPage = new NavigationPage(mainPage);
                HttpClient httpClient = new HttpClient((new HttpClientHandler()))
                {
                    BaseAddress = new Uri("http://10.0.2.2:55939")
                };

                //httpClient.DefaultRequestHeaders.Accept.Clear();
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //httpClient.DefaultRequestHeaders

                Services.RegisterInstance(navigationPage.Navigation);
                Services.Register<IViewMapper, ViewMapper>(Lifestyle.Singleton);
                Services.RegisterInstance(httpClient);

                // Setup the initial binding context
                mainPage.BindingContext = Services.GetInstance<AnmeldenViewModel>();

                // Assign the main page
                MainPage = navigationPage;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
