using Jx.Gateway.ReverseProxy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddReverseProxy().LoadGateway();
builder.Services.AddBootstrapBlazor();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapReverseProxy();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
