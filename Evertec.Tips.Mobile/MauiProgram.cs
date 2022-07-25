using CommunityToolkit.Maui;
using Evertec.Tips.Infrastructure.Interfaces;
using Evertec.Tips.Infrastructure.Providers;
using Evertec.Tips.Infrastructure.Repositories;
using Evertec.Tips.Mobile.Interfaces;
using Evertec.Tips.Mobile.Providers.Dialog;
using Evertec.Tips.Mobile.Providers.Navigation;
using Evertec.Tips.Mobile.Providers.Toast;
using Evertec.Tips.Mobile.Services;
using Evertec.Tips.Mobile.ViewModels;
using Evertec.Tips.Mobile.ViewModels.Base;

namespace Evertec.Tips.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<AddTipViewModel>();
        builder.Services.AddTransient<DetailTipViewModel>();
        builder.Services.AddTransient<TipsViewModel>();
        builder.Services.AddTransient<BaseViewModel>();

        builder.Services.AddSingleton<IDialogProvider, DialogProvider>();
        //builder.Services.AddSingleton<IProgressProvider, ProgressProvider>();
        builder.Services.AddSingleton<IToastProvider, ToastProvider>();
        builder.Services.AddSingleton<INavigationProvider, NavigationProvider>();
        builder.Services.AddSingleton<IDatabaseContextProvider, DatabaseContextProvider>();

        builder.Services.AddSingleton<IAuthorService, AuthorService>();
        builder.Services.AddSingleton<ITipService, TipService>();
        builder.Services.AddSingleton<IRealTimeTipService, RealTimeTipService>();

        builder.Services.AddSingleton<ITipRepository, TipRepository>();
        builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();

        return builder.Build();
    }


}
