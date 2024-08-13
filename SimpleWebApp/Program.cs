using Microsoft.EntityFrameworkCore;
using SimpleWebApp.core.Context;
using SimpleWebApp.core.Interface;
using SimpleWebApp.core.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register the DbContext with DI
builder.Services.AddDbContext<SimpleDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SimpleDatabaseConstring"));
});

// Register the service with DI
builder.Services.AddScoped<ISimpleWebAppService, SimpleWebAppService>();
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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
