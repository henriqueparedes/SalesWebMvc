﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext"), new MySqlServerVersion(new Version())));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartamentService>();
builder.Services.AddScoped<SalesRecordService>();

var app = builder.Build();

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

static void Seed(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    SeedingService seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();

    seedingService.Seed();
}

Seed(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
