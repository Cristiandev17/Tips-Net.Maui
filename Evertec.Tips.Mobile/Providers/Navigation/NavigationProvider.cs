using System.Globalization;
using System.Reflection;
using Evertec.Tips.Mobile.ViewModels.Base;

namespace Evertec.Tips.Mobile.Providers.Navigation
{
    public class NavigationProvider : INavigationProvider
    {
        
        private IServiceProvider _provider;

        public NavigationPage CurrentPage => Application.Current?.MainPage as NavigationPage;

        public NavigationProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task ClearNavigationAsync()
        {
            if (Application.Current?.MainPage is NavigationPage mainPage)
            {
                var count = mainPage.Navigation.NavigationStack.Count - 1;

                for (int i = count; i > 0; i--)
                {
                    var page = mainPage.Navigation.NavigationStack[i - 1];
                    mainPage.Navigation.RemovePage(page);
                }
            }
            return Task.CompletedTask;
        }

        public Task GoBackAsync()
        {
            if (Application.Current?.MainPage is NavigationPage mainPage)
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 1]);

            return Task.CompletedTask;
        }

        public Task GoBackModalAsync()
        {
            if (Application.Current?.MainPage is NavigationPage mainPage)
            {
                mainPage.Navigation.PopModalAsync(true);
            }

            return Task.CompletedTask;
        }

        public Task NavigateModalPageAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), null);
        }

        public Task NavigateModalPageAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameter);
        }


        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = await Navigate(viewModelType);

            if (page.BindingContext is BaseViewModel viewModelBase)
            {
                await viewModelBase.Initialize(parameter);
            }
        }

        private async Task InternalNavigateToModalAsync(Type viewModelType, object parameter)
        {
            Page page = await NavigateModal(viewModelType);

            if (page.BindingContext is BaseViewModel viewModelBase && parameter != null)
            {
                await viewModelBase.Initialize(parameter);
            }

        }

        private async Task<Page> Navigate(Type viewModelType)
        {
            Page page = CreatePage(viewModelType);
            page.BindingContext = _provider.GetService(viewModelType);

            if (CurrentPage != null)
            {
                CurrentPage.BackgroundColor = Color.FromRgb(255,255,2555);
                await CurrentPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            return page;
        }

        private async Task<Page> NavigateModal(Type viewModelType)
        {
            Page page = CreatePage(viewModelType);
            NavigationPage.SetHasNavigationBar(page, false);

            page.BindingContext = _provider.GetService(viewModelType);

            await CurrentPage.Navigation.PushModalAsync(page, true);

            return page;
        }

        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new NotSupportedException($"Cannot locate page type for {viewModelType}");
            }

            return (Page)Activator.CreateInstance(pageType);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

    }
}
