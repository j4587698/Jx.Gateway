using Docker.DotNet;
using Docker.DotNet.Models;

namespace Jx.Gateway.Docker;

public static class DockerExtensions
{
    public static IServiceCollection AddDocker(this IServiceCollection services)
    {
        var uri = OperatingSystem.IsWindows() ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:///var/run/docker.sock");
        DockerClient client = new DockerClientConfiguration(uri).CreateClient();
        services.AddSingleton(client);
        return services;
    }
}