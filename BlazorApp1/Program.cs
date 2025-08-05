using BlazorApp1.Components;
using BlazorApp1.Data;
using BlazorApp1.Entities;
using BlazorApp1.Services;
using BlazorApp1.ValidationAttributes;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//-------------------------------------------------------------------
var env = builder.Environment;
var dbPath = Path.Combine(env.ContentRootPath, "App_Data", "1404.db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddRazorPages(); //  this is required for Identity UI Razor Pages

builder.Services.AddScoped<DataServices>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password rules
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    // Username allowed characters
    options.User.AllowedUserNameCharacters = "0123456789";

    // You can also configure other options here
});
//-------------------------------------------------------------------
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//-------------------------------------------------------------------
app.UseAuthentication();
app.UseAuthorization();
//-------------------------------------------------------------------

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//-------------------------------------------------------------------
app.MapRazorPages(); // this makes /Identity/Account/Login work
app.MapControllers();
//-------------------------------------------------------------------

app.Run();
