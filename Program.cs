using BlogApp.Components;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Services;
using Microsoft.AspNetCore.Components.Authorization; // Add this
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this
using Microsoft.IdentityModel.Tokens;                 // Add this
using System.Text;                                   // Add this

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core config
builder.Services.AddDbContextFactory<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("identitycs")));

// --- Add Blazor Authentication Core services ---
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
// Re-map standard AuthenticationStateProvider to use your custom Jwt provider
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<SimpleAuthService>();

// --- Register ASP.NET Core authentication/authorization services ---
// These are required by app.UseAuthentication()/UseAuthorization() in the middleware pipeline.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "ReplaceWithYourKey"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

// --- Add Authentication Middleware ---
app.UseAuthentication(); // Put before Authorization if you add authorization policies
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();