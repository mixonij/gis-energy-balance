using Ispu.Gis.EnergyBalances.Application.Storages;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EnergyBalancesContext>(options =>
    options.UseNpgsql(connectionString, o => o.UseNetTopologySuite()));

builder.Services.AddSwaggerDocument(settings =>
{
    settings.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Energy Balances API";
        document.Info.Description = "REST API Energy Balances.";
    };
});

builder.Services.AddSingleton<IPipesStorage, PipesStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Lifetime.ApplicationStarted.Register(OnAppStarted);

app.Run();


async void OnAppStarted()
{
    var pipesService = app.Services.GetRequiredService<IPipesStorage>();

    //await pipesService.Initialize();
}