namespace FlashBeat.Maui;

using FlashBeat.Web.Service;

using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.AddAppSettingsConfiguration();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddWebServices();

        builder.Services.AddFluentUIComponents();

        return builder.Build();
    }
}
