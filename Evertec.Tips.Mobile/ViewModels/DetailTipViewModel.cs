using Evertec.Tips.Domain.Models;
using Evertec.Tips.Mobile.Domain.Enumerations;
using Evertec.Tips.Mobile.Domain.Helpers;
using Evertec.Tips.Mobile.ViewModels.Base;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Evertec.Tips.Mobile.ViewModels
{
    public partial class DetailTipViewModel : BaseViewModel
    {
        [ObservableProperty]
        private TipModel _tip;

        [ObservableProperty]
        private AuthorModel _author;

        public DetailTipViewModel(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override async Task Initialize(object parameters)
        {
            Authors = await AuthorService.GetAll();
            if (parameters != null && parameters is Dictionary<string, object> navigationData)
            {
                Tip = navigationData[NavigationParametersHelper.DetailTip] as TipModel;
                Author = Authors.FirstOrDefault(a => a.Id == Tip.AuthorId);
            }
        }

        [ICommand]
        public async Task EditTip()
        {
            await ShowProgress();
            await NavigationProvider.NavigateToAsync<AddTipViewModel>(new Dictionary<string, object>() { { NavigationParametersHelper.EditTip, Tip }, { NavigationParametersHelper.Action, Actions.Edit } });
            HideProgress();
        }
    }
}
