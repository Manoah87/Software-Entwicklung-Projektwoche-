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
            }

            return result;
        }
    }
}
