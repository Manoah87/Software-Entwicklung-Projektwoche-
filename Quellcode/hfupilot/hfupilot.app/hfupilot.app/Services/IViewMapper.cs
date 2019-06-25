using Xamarin.Forms;

namespace hfupilot.app.Services
{
    public interface IViewMapper
    {
        Page Map<TViewModel>(TViewModel viewModel) where TViewModel : class;
    }
}
