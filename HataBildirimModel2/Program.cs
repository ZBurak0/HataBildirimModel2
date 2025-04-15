using HataBildirimModel.Models;
using HataBildirimModel2.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<FaultInterface, FaultRepository>();

//sql server� ba�layan kod 
builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Sqlcon"));
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<FaultRepository>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
