using GinasioVitaFit.Shared.Services;
using GinasioVitaFit.WebClient.Components;
using MudBlazor.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRefitClient<IGinasioVitaFitService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7134"));


builder.Services.AddRefitClient<IAuthApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7134"));

// 3. Registo dos Serviços do MudBlazor
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", "?statusCode={0}");
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();