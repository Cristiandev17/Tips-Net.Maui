using Evertec.Tips.Domain.Models;
using Evertec.Tips.Mobile.Domain.Enumerations;
using Evertec.Tips.Mobile.Domain.Exceptions;
using Evertec.Tips.Mobile.Domain.Helpers;
using Evertec.Tips.Mobile.Helpers;
using Evertec.Tips.Mobile.Providers.Toast;
using Evertec.Tips.Mobile.ViewModels.Base;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Evertec.Tips.Mobile.ViewModels
{
    public partial class AddTipViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _titleTip;

        [ObservableProperty]
        private AuthorModel _selectedAuthor;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private DateTime _date = DateTime.Now;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now.Date;

        private int _id;

        private Actions _action;

        private IToastProvider _toastProvider;

        public AddTipViewModel(IServiceProvider serviceProvider, IToastProvider toastProvider) : base(serviceProvider)
        {
            _toastProvider = toastProvider;
        }

        public override async Task Initialize(object parameters)
        {
            Authors = await AuthorService.GetAll();
            if (parameters != null && parameters is Dictionary<string, object> navigationData)
            {
                _action = (Actions)navigationData[NavigationParametersHelper.Action];
                if (_action == Actions.Edit)
                {
                    Title = TextFieldHelper.Get("strEditTip");
                    var tip = navigationData[NavigationParametersHelper.EditTip] as TipModel;
                    EditAction(tip);
                }
                else
                {
                    Description = "Lattice angels seeming devil soul lining again maiden the these cannot nevermore my caught i a never flirt thereat his wretch fiery the and ashore";
                    Title = TextFieldHelper.Get("strNewTip");
                }
            }
        }

        private void EditAction(TipModel tip)
        {
            if (tip != null)
            {
                TitleTip = tip.Title;
                Description = tip.Description;
                SelectedAuthor = Authors.FirstOrDefault(a => a.Id == tip.AuthorId);
                Date = tip.CreationDate;
                _id = tip.Id;
            }
        }

        [ICommand]
        public async Task Save()
        {
            try
            {
                var response = await DialogProvider.DisplayAlertAsync(TextFieldHelper.Get("strWarning"),
                    TextFieldHelper.Get("strMessageSaveTip"), TextFieldHelper.Get("strAccept"),
                    TextFieldHelper.Get("strCancel"));
                if (response)
                {
                    await ShowProgress();
                    var tip = new TipModel()
                    {
                        Title = TitleTip ?? string.Empty,
                        CreationDate = Date,
                        Description = Description ?? string.Empty,
                        UpdateDate = DateTime.Now,
                        AuthorId = _selectedAuthor?.Id ?? 0,
                    };
                    switch (_action)
                    {
                        case Actions.Edit:
                            await UpdateTip(tip);
                            break;

                        case Actions.New:
                            await CreateTip(tip);
                            break;
                    }
                }
            }
            catch (AppException ex)
            {
                await _toastProvider.LongTime(TextFieldHelper.Get(ex.Code));
            }
            catch (Exception ex)
            {
                await _toastProvider.LongTime(ex.Message);
            }
            finally
            {
                HideProgress();
            }

        }

        private async Task CreateTip(TipModel tip)
        {
            var result = await TipService.AddTip(tip);
            if (result)
            {
                await _toastProvider.LongTime(TextFieldHelper.Get("strSuccessCreateTip"));
                await NavigationProvider.GoBackAsync();
            }
            else
                await _toastProvider.LongTime(TextFieldHelper.Get("strError"));
        }

        private async Task UpdateTip(TipModel tip)
        {

            tip.Id = _id;
            var result = await TipService.UpdateTip(tip);
            if (result)
            {
                await _toastProvider.LongTime(TextFieldHelper.Get("strSuccessUpdateTip"));
                //await NavigationService.GoBackAsync(new NavigationParameters { { NavigationParametersHelper.DetailTip, tip } });
            }
            else
                await _toastProvider.LongTime(TextFieldHelper.Get("strError"));
        }
    }
}
