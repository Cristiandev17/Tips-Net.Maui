using Evertec.Tips.Mobile.Providers.Navigation;
using Evertec.Tips.Mobile.ViewModels;

namespace Evertec.Tips.Mobile;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        var navigationProvider = serviceProvider.GetService<INavigationProvider>();
        navigationProvider?.NavigateToAsync<LoginViewModel>();
    }
}
 