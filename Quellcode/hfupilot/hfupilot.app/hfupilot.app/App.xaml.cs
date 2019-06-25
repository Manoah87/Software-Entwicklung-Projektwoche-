using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Services;
using hfupilot.app.ViewModels;
using hfupilot.app.Views;
using SimpleInjector;
using Xamarin.Forms;

namespace hfupilot.app
{
    public partial class App : Application
    {
        public static Container Services { get; } = new Container();
        
        public App()
        {
            InitializeComponent();

            if (!DesignMode.IsDesignModeEnabled)
            {
                var mainPage = new AnmeldenView();
                var navigationPage = new NavigationPage(mainPage);

                Services.RegisterInstance(navigationPage.Navigation);

                Services.Register<IViewMapper, ViewMapper>(Lifestyle.Singleton);

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
