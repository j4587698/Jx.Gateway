using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.ReverseProxy;

public static class GatewayConfigProviderExtensions
{
    public static IReverseProxyBuilder LoadGateway(this IReverseProxyBuilder builder)
    {
        builder.Services.AddSingleton<IProxyConfigProvider>(new GatewayConfigProvider());
        return builder;
    }
}