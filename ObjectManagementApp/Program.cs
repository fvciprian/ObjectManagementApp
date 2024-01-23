using Microsoft.EntityFrameworkCore;
using ObjectManagementApp.Data;
using ObjectManagementApp.Models;
using ObjectManagementApp.Services;
using ObjectManagementApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ObjectManagementAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ObjectManagementAppContext") ?? throw new InvalidOperationException("Connection string 'ObjectManagementAppContext' not found.")));

// add DI
builder.Services.AddScoped<IObjectService, ObjectService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
    pattern: "{controller=CustomObjects}/{action=Index}/{id?}");

app.Run();
