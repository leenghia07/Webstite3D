using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Identity.Data;
using WebApp.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'UserDataContextConnection' not found.");

builder.Services.AddDbContext<UserDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<MuseumDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserDataContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
                .AddRazorPagesOptions(
                options =>
                {
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login");
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/Register");
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
app.UseAuthentication();;
app.MapRazorPages();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
// app.MapAreaControllerRoute(
//             name: "demo",
//             areaName: "Identity",
//             pattern: "Login/{controller=Home}/{action=Index}/{id?}");
app.Run();
