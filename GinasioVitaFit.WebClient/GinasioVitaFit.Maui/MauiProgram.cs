using GinasioVitaFit.Shared.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Refit;

namespace GinasioVitaFit.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddMudServices();
        
        builder.Services.
            AddRefitClient<IGinasioVitaFitService>()
            .AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7134"));


        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}