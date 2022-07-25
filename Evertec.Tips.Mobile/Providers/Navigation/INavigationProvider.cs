using Evertec.Tips.Mobile.ViewModels.Base;

namespace Evertec.Tips.Mobile.Providers.Navigation
{
    public interface INavigationProvider
    {
        NavigationPage CurrentPage { get; }

        Task ClearNavigationAsync();

        Task GoBackAsync();

        Task GoBackModalAsync();
       
        Task NavigateModalPageAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateModalPageAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
       
    }
}
