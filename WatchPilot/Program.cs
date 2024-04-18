using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using WatchPilot.Controllers;
using WatchPilot.Data;
using WatchPilot.Data.DataAccessObjects;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IShowDAO, ShowDAO>();
builder.Services.AddScoped<IImageLogic, ImageLogic>();
builder.Services.AddScoped<IShowLogic, ShowLogic>();
builder.Services.AddScoped<IShowOverviewDAO, ShowOverviewDAO>();
builder.Services.AddScoped<IShowOverviewLogic, ShowOverviewLogic>();
builder.Services.AddScoped(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    return new DatabaseConnection(configuration);
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
}); // This is so i can use a Dir thats not in wwwroot. I just think the file structure is cleaner this way.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
