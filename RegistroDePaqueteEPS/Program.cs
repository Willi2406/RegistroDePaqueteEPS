using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Components;
using RegistroDePaqueteEPS.Components.Account;
using RegistroDePaqueteEPS.Data;
using RegistroDePaqueteEPS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddScoped<PaquetesService>();
builder.Services.AddScoped<PreavisosService>();
builder.Services.AddScoped<AutorizadosEntregaService>();
builder.Services.AddScoped<DireccionesDeliveryService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<ApplicationDbContext>(o => o.UseSqlite(ConStr));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
var services = scope.ServiceProvider;
try
{
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
string[] roleNames = { "Admin", "Usuario" };
foreach (var roleName in roleNames)
{
var roleExist = await roleManager.RoleExistsAsync(roleName);
if (!roleExist)
{
await roleManager.CreateAsync(new IdentityRole(roleName));
}
}

var emailAdmin = "warodriguezv@gmail.com";
 


        var user = await userManager.FindByEmailAsync(emailAdmin);
if (user != null)
{
if (!await userManager.IsInRoleAsync(user, "Admin"))
{
await userManager.AddToRoleAsync(user, "Admin");
}
}
}
catch (Exception ex)
{
var logger = services.GetRequiredService<ILogger<Program>>();
logger.LogError(ex, "Ocurrió un error al crear los roles del sistema.");
}
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    // ... resto del archivo ...


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
