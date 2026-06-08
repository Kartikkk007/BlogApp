using BlogApp.Components;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ef core config withsql server
builder.Services.AddDbContextFactory<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("identitycs")));

builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<SimpleAuthService>();

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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
