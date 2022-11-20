using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'UserDataContextConnection' not found.");

builder.Services.AddDbContext<UserDatacontext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Gebruiker instellingen
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters =
          "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserDatacontext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy => policy.RequireClaim("Manager"));
    options.AddPolicy("Employee", policy => policy.RequireClaim("Employee"));
    options.AddPolicy("User", policy => policy.RequireClaim("User"));
});
builder.Services.AddDbContext<MuseumDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDirectoryBrowser();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
                .AddRazorPagesOptions(
                options =>
                {
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login");
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/Register");
                });
StaticFileOptions options = new StaticFileOptions 
    { 
        ContentTypeProvider = new FileExtensionContentTypeProvider()
    };
((FileExtensionContentTypeProvider)options.ContentTypeProvider)
    .Mappings.Add(new KeyValuePair<string, string>(".glb", "model/gltf-buffer"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles(options);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDefaultFiles();

app.UseRouting();
app.UseAuthentication();

app.MapRazorPages();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
