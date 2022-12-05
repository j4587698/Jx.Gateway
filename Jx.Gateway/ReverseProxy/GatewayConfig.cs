using Jx.Gateway.Utils;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.ReverseProxy;

public class GatewayConfig : IProxyConfig
{
    private readonly CancellationTokenSource _cts = new();
    
    public GatewayConfig()
    {
        var gatewayEntities = LitedbHelper.GetGatewayCollection().FindAll().ToList();
        Clusters = gatewayEntities.Select(x => x.ToCluster()).ToArray();
        Routes = gatewayEntities.Select(x => x.ToRoute()).ToArray();
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