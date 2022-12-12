using System.Security.Cryptography.X509Certificates;
using BootstrapBlazor.Components;
using Jx.Gateway.Docker;
using Jx.Gateway.ReverseProxy;
using Jx.Gateway.Services;
using Constants = Jx.Gateway.Utils.Constants;

var builder = WebApplication.CreateBuilder(args);
builder.UseKestrelHttps();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddReverseProxy().LoadGateway();
builder.Services.AddBootstrapBlazor();
builder.Services.AddDocker();
builder.Services.AddSingleton<ILookupService, LookupService>();
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapReverseProxy();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

