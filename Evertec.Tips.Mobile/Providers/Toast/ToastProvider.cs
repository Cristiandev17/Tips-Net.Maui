using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Evertec.Tips.Mobile.Providers.Toast
{
    public class ToastProvider : IToastProvider
    {
        public Task LongTime(string message)
        {
            return Task.FromResult(CommunityToolkit.Maui.Alerts.Toast.Make(message, ToastDuration.Long).Show());
        }
    }
}
