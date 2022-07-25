namespace Evertec.Tips.Mobile.Controls.ContentViews;

public partial class LoaderView : ContentView
{
    public LoaderView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        nameof(IndicatorColor),
        typeof(Color),
        typeof(LoaderView),
        Color.FromRgb(10, 10, 10),
        BindingMode.OneWay);

    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty);
        set => SetValue(IndicatorColorProperty, value);
    }


    public static readonly BindableProperty LoaderTextProperty = BindableProperty.Create(
        nameof(LoaderText),
        typeof(string),
        typeof(LoaderView),
        string.Empty,
        BindingMode.OneWay);

    public string LoaderText
    {
        get => (string)GetValue(LoaderTextProperty);
        set => SetValue(LoaderTextProperty, value);
    }


    public static readonly BindableProperty LoaderFontSizeProperty = BindableProperty.Create(
        nameof(LoaderFontSize),
        typeof(int),
        typeof(LoaderView),
        10,
        BindingMode.OneWay);

    public int LoaderFontSize
    {
        get => (int)GetValue(LoaderFontSizeProperty);
        set => SetValue(LoaderFontSizeProperty, value);
    }
}