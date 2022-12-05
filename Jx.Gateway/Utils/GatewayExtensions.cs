using Jx.Gateway.Entity;
using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.Utils;

public static class GatewayExtensions
{
    public static ClusterConfig ToCluster(this GatewayEntity gatewayEntity)
    {
        var config = new ClusterConfig()
        {
            ClusterId = gatewayEntity.ClusterId,
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                {"default", new DestinationConfig()
                {
                    Address = gatewayEntity.Url
                }}
            }
        };
        return config;
    }

    public static RouteConfig ToRoute(this GatewayEntity gatewayEntity)
    {
        var config = new RouteConfig()
        {
            RouteId = gatewayEntity.RouteId,
            ClusterId = gatewayEntity.ClusterId,
            Order = -200,
            Match = new RouteMatch()
            {
                Hosts = gatewayEntity.Hosts,
                Path = "{**catch-all}"
            }
        };
        return config;
    }
}