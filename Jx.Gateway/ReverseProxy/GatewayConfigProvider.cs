using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.ReverseProxy;

public class GatewayConfigProvider : IProxyConfigProvider
{
    private volatile GatewayConfig _config;

    public GatewayConfigProvider()
    {
        _config = new GatewayConfig();
    }
    
    public IProxyConfig GetConfig() => _config;
}