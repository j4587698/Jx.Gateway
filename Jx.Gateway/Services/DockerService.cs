using Docker.DotNet;
using Docker.DotNet.Models;
using Furion.DependencyInjection;
using Jx.Gateway.Utils;

namespace Jx.Gateway.Services;

public class DockerService: ITransient
{
    
    public DockerService(DockerClient client)
    {
        Client = client;
    }

    private DockerClient Client { get; set; }

    /// <summary>
    /// 获取所有容器
    /// </summary>
    /// <param name="all">是否获取未运行容器（默认为true）</param>
    /// <returns></returns>
    public async Task<IList<ContainerListResponse>> GetContainers(bool all = true)
    {
        var containers = await Client.Containers.ListContainersAsync(new ContainersListParameters()
        {
            All = all
        });
        return containers;
    }
    
    public async Task RefreshInfo()
    {
        try
        {
            var info = await Client.System.GetSystemInfoAsync();
            Constants.SystemInfo = info;
        }
        catch (Exception e)
        {
            
        }
    }

    /// <summary>
    /// 获取容器日志
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns></returns>
    public async Task<Stream> GetContainerLogs(string containerId)
    {
        var logs = await Client.Containers.GetContainerLogsAsync(containerId, new ContainerLogsParameters
        {
            ShowStdout = true,
            ShowStderr = true,
            Follow = true,
            Timestamps = true,
            Tail = "all"
        });
        return logs;
    }
}