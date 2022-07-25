using Evertec.Tips.Domain.Models;
using Evertec.Tips.Mobile.Interfaces;
using Evertec.Tips.Mobile.Providers.Dialog;
using Evertec.Tips.Mobile.Providers.Navigation;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Evertec.Tips.Mobile.ViewModels.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private List<AuthorModel> _authors;

        [ObservableProperty]
        private bool _isBusy;


        protected ITipService TipService;
        protected IAuthorService AuthorService;

        protected readonly IDialogProvider DialogProvider;
        protected readonly INavigationProvider NavigationProvider;

        public BaseViewModel(IServiceProvider serviceProvider)
        {
            this.NavigationProvider = serviceProvider.GetService<INavigationProvider>();
            this.TipService = serviceProvider.GetService<ITipService>();
            this.DialogProvider = serviceProvider.GetService<IDialogProvider>();
            this.AuthorService = serviceProvider.GetService<IAuthorService>();
        }

        protected async Task ShowProgress()
        {
            IsBusy = true;
            await Task.Delay(5000);
        }


        protected void HideProgress()
        {
            IsBusy = false;
        }
        
        protected async Task CreateAuthors()
        {
            Authors = await AuthorService.GetAll();
            if (!Authors.Any())
            {
                var one = AuthorService.AddAuthor(new AuthorModel
                {
                    Name = "Juan Ortega",
                });
                var two = AuthorService.AddAuthor(new AuthorModel
                {
                    Name = "Juan Alvarez",
                });
                var three = AuthorService.AddAuthor(new AuthorModel
                {
                    Name = "Juan Osorio",
                });
                var four = AuthorService.AddAuthor(new AuthorModel
                {
                    Name = "Juan Garcia",
                });
                await Task.WhenAll(one, two, three, four);
            }
        }

        public virtual Task Initialize(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
