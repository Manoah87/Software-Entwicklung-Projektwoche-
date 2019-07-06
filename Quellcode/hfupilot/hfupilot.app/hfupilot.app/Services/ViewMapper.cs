using hfupilot.app.ViewModels;
using hfupilot.app.Views;
using Xamarin.Forms;

namespace hfupilot.app.Services
{
    class ViewMapper : IViewMapper
    {
        public Page Map<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            Page result = null;
            switch (viewModel)
            {
                case AnmeldenViewModel mainViewModel:
                    result = new AnmeldenView();
                    result.BindingContext = mainViewModel;
                    break;
                case DashboardViewModel dashboardViewModel:
                    result = new DashboardView();
                    result.BindingContext = dashboardViewModel;
                    dashboardViewModel.Update();
                    break;
                case StundenplanViewModel stundenplanViewModel:
                    result = new StundenplanView();
                    result.BindingContext = stundenplanViewModel;
                    stundenplanViewModel.Update(true);
                    break;
                case MeldungViewModel meldungViewModel:
                    result = new MeldungView();
                    result.BindingContext = meldungViewModel;
                    break;
                case AbwesenheitViewModel abwesenheitViewModel:
                    result = new AbwesenheitView();
                    result.BindingContext = abwesenheitViewModel;
                    break;
                case VerspaetungViewModel verspaetungViewModel:
                    result = new VerspaetungView();
                    result.BindingContext = verspaetungViewModel;
                    break;
                case MeldungenAnzeigenViewModel meldungenAnzeigenViewModel:
                    result = new MeldungenAnzeigenView();
                    result.BindingContext = meldungenAnzeigenViewModel;
                    meldungenAnzeigenViewModel.Update();
                    break;
            }


            return result;
        }
    }
}
