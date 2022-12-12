using System.Security.Cryptography.X509Certificates;
using Jx.Gateway.Utils;

namespace Jx.Gateway.ReverseProxy;

public static class KestrelExtensions
{
    public static WebApplicationBuilder UseKestrelHttps(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ConfigureHttpsDefaults(configureOptions =>
            {
                configureOptions.ServerCertificateSelector = (context, name) => Constants.Certificates.TryGetValue(name, out X509Certificate2 value) ? value : null;
            });
        });

        return builder;
    }
}