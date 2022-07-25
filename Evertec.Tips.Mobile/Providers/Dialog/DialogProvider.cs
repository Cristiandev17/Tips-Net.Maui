using Evertec.Tips.Mobile.Providers.Navigation;

namespace Evertec.Tips.Mobile.Providers.Dialog
{
    public class DialogProvider : IDialogProvider
    {
        private INavigationProvider _navigationProvider;

        public DialogProvider(INavigationProvider navigationProvider)
        {
            _navigationProvider = navigationProvider;
        }

        public async Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            return await _navigationProvider.CurrentPage.DisplayActionSheet(title, cancelButton, destroyButton, otherButtons);
        }

        public async Task<string> DisplayPrompt(string title, string message)
        {
            return await _navigationProvider.CurrentPage.DisplayPromptAsync(title, message);
        }

        public async Task DisplayAlertAsync(string title, string message)
        {
            await _navigationProvider.CurrentPage.DisplayAlert(title, message, "Ok");
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            return await _navigationProvider.CurrentPage.DisplayAlert(title, message, acceptButton, cancelButton);
        }
    }
}
