using Evertec.Tips.Mobile.ViewModels;
namespace Evertec.Tips.Mobile.Views;

public partial class TipsView : ContentPage
{
    public TipsView()
    {
        InitializeComponent();
    }


    protected override async void OnDisappearing()
    {
        var vm = (TipsViewModel)BindingContext;
        await vm.LoadData();
    }
}