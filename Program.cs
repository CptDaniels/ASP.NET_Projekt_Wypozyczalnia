using ASP.NET_Projekt_Wypozyczalnia.Data;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Repositories;
using ASP.NET_Projekt_Wypozyczalnia.Services;
using ASP.NET_Projekt_Wypozyczalnia.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PredatorConnection")));
//"ThinkpadConnection"   "PredatorConnection"

//Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRentalItemsRepository, RentalItemsRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRentalItemsService, RentalItemsService>();

//Walidacja
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();

MapsterConfig.Configure();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
