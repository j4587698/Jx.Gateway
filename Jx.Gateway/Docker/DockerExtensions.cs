using Docker.DotNet;
using Docker.DotNet.Models;
using Jx.Gateway.Services;
using Jx.Gateway.Utils;

namespace Jx.Gateway.Docker;

public static class DockerExtensions
{
    public static async Task<IServiceCollection> AddDocker(this IServiceCollection services)
    {
        var uri = OperatingSystem.IsWindows() ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:///var/run/docker.sock");
        DockerClient client = new DockerClientConfiguration(uri).CreateClient();
        services.AddSingleton(client);
        
        var service = Furion.App.GetService<DockerService>();
        await service.RefreshInfo();
        return services;
    }
}