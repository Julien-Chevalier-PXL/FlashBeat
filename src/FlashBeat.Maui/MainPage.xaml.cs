namespace FlashBeat.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        Loaded += this.MainPage_Loaded;
        this.InitializeComponent();
    }

    private async void MainPage_Loaded(object? sender, EventArgs e)
    {
#if WINDOWS
        var webview = this.blazorWebView.Handler?.PlatformView as Microsoft.UI.Xaml.Controls.WebView2;
        if (webview is not null)
        {
            await webview.EnsureCoreWebView2Async();

            webview.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;
        }
#endif
    }
}
