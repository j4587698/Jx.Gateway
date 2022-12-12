using Docker.DotNet;
using Docker.DotNet.Models;
using Jx.Gateway.Utils;

namespace Jx.Gateway.Docker;

public static class DockerExtensions
{
    public static async Task<IServiceCollection> AddDocker(this IServiceCollection services)
    {
        var uri = OperatingSystem.IsWindows() ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:///var/run/docker.sock");
        DockerClient client = new DockerClientConfiguration(uri).CreateClient();
        services.AddSingleton(client);
        try
        {
            var info = await client.System.GetSystemInfoAsync();
            Constants.SystemInfo = info;
        }
        catch (Exception e)
        {
            
        }
        
        return services;
    }
}