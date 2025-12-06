using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using RegistroDePaqueteEPS.Components;
using RegistroDePaqueteEPS.Components.Account;
using RegistroDePaqueteEPS.Data;
using RegistroDePaqueteEPS.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Servicios Base y Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddMudServices();

// 3. Servicios de Negocio (Tus servicios propios)
// Podrías mover esto a un método de extensión si la lista crece mucho
builder.Services.AddScoped<PaquetesService>();
builder.Services.AddScoped<PreavisosService>();
builder.Services.AddScoped<AutorizadosEntregaService>();
builder.Services.AddScoped<DireccionesDeliveryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailManager>();

// 4. Configuración de Base de Datos e Identity
var connectionString = builder.Configuration.GetConnectionString("ConStr")
    ?? throw new InvalidOperationException("Connection string 'ConStr' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();

// 5. Inicialización de Datos (Seeding)
// Ejecutamos esto en un scope separado antes de correr la app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbSeeder.SeedRolesAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error durante la inicialización de la base de datos.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/not-found");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();