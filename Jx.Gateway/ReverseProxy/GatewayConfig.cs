using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.ReverseProxy;

public class GatewayConfig : IProxyConfig
{
    private readonly CancellationTokenSource _cts = new();
    
    public GatewayConfig()
    {
        Clusters = new[]
        {
            new ClusterConfig()
            {
                ClusterId = "bb",
                Destinations = new Dictionary<string, DestinationConfig>()
                {
                    {"baidu", new DestinationConfig() {Address = "https://www.blazor.zone"}}
                }
            }
        };
        Routes = new []{new RouteConfig()
        {
            RouteId = "bb",
            ClusterId = "bb",
            // Order = -200,
            Match = new RouteMatch()
            {
                Path = "{**catch-all}"
            }
        }};
        ChangeToken = new CancellationChangeToken(_cts.Token);
    }
    
    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; }

    /// <summary>
    /// config更新
    /// </summary>
    internal void Changed()
    {
        _cts.Cancel();
    }
}