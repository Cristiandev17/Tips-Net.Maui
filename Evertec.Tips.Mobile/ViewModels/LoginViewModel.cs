using Evertec.Tips.Mobile.Providers.Toast;
using Evertec.Tips.Mobile.ViewModels.Base;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Evertec.Tips.Mobile.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        private IToastProvider _toastProvider;

        public LoginViewModel(IServiceProvider serviceProvider, IToastProvider toastProvider) : base(serviceProvider)
        {
            _toastProvider = toastProvider;
        }

        [ICommand]
        public async Task Login()
        {
            await ShowProgress();

            if (!string.IsNullOrEmpty(UserName) && UserName.Length >= 7)
            {
                await NavigationProvider.NavigateToAsync<TipsViewModel>();
            }
            else
            {
                await _toastProvider.LongTime("");
            }

            HideProgress();
        }

    }
}
