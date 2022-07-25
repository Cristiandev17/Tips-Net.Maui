using Evertec.Tips.Mobile.Domain.Enumerations;
using Evertec.Tips.Mobile.Domain.Helpers;
using Evertec.Tips.Mobile.Helpers;
using Evertec.Tips.Mobile.Providers.Toast;
using Evertec.Tips.Mobile.ViewModels.Base;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Evertec.Tips.Domain.Models;

namespace Evertec.Tips.Mobile.ViewModels
{
    public partial class TipsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TipModel> _tips = new();

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _isThereTips;

        private IToastProvider _toastProvider;

        public TipsViewModel(IServiceProvider serviceProvider, IToastProvider toastProvider) : base(serviceProvider)
        {
            _toastProvider = toastProvider;
        }

        [ICommand]
        public async Task RefreshTips()
        {
            Tips = await TipService.GetAll();
        }

        public override async Task Initialize(object parameters)
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            await ShowProgress();
            await CreateAuthors();
            await RefreshView();
            HideProgress();
        }


        private async Task RefreshView()
        {
            await RefreshTips();
            if (Tips != null) IsThereTips = Tips.Any() ? true : false;
        }

        [ICommand]
        public async Task DeleteTip(TipModel item)
        {
            await ShowProgress();
            var response = await DialogProvider.DisplayAlertAsync(TextFieldHelper.Get("strWarning"), TextFieldHelper.Get("strMessageDeleteTip"),
                                 TextFieldHelper.Get("strDelete"), TextFieldHelper.Get("strCancel"));
            if (response)
            {
                var tip = Tips.FirstOrDefault(t => t.Id == item.Id);
                if (tip != null)
                {
                    var result = await TipService.DeleteTip(tip.Id);
                    if (result)
                    {
                        Tips.Remove(tip);
                        await _toastProvider.LongTime(TextFieldHelper.Get("strSuccessDeleteTip"));
                    }
                    else
                        await _toastProvider.LongTime(TextFieldHelper.Get("strError"));
                }
            }
            await RefreshView();
            HideProgress();
        }

        [ICommand]
        public async Task DetailTip(TipModel item)
        {
            await ShowProgress();
            await NavigationProvider.NavigateToAsync<DetailTipViewModel>(new Dictionary<string, object> { { NavigationParametersHelper.DetailTip, item } });
            HideProgress();
        }

        [ICommand]
        public async Task CreateTip()
        {
            await ShowProgress();
            await NavigationProvider.NavigateToAsync<AddTipViewModel>(new Dictionary<string, object> { { NavigationParametersHelper.Action, Actions.New } });
            HideProgress();
        }
    }
}
