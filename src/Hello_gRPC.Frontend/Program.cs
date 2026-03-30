using HelloGrpc.Frontend.Components;
using HelloGrpc.Frontend.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
});

// gRPC client
builder.Services.AddGrpcClient<HelloGrpc.Shared.PersonalityGrpc.PersonalityGrpcClient>(options =>
{
    options.Address = new Uri("http://localhost:5001");
});

builder.Services.AddScoped<PersonalityGrpcClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
