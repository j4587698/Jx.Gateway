using System.Security.Cryptography.X509Certificates;
using BootstrapBlazor.Components;
using Jx.Gateway.Acme;
using Jx.Gateway.Docker;
using Jx.Gateway.ReverseProxy;
using Jx.Gateway.Services;

var builder = WebApplication.CreateBuilder(args).Inject().UseKestrelHttps();
builder.Services.AddControllers().AddInject();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddReverseProxy().LoadGateway();
builder.Services.AddBootstrapBlazor();
await builder.Services.AddDocker();
builder.Services.AddSingleton<ILookupService, LookupService>();
builder.Services.AddFileLogging("./log/application-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
{
    options.FileNameRule = s => String.Format(s, DateTime.Now);
});
builder.Services.AddConsoleFormatter();

var app = builder.Build();

app.UseStaticFiles();

app.UseInject();

app.UseRouting();

app.MapControllers();
app.UseAcmeChallengeHandler();
app.MapReverseProxy();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

