using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Yarp.ReverseProxy.Configuration;

namespace Jx.Gateway.Entity;

/// <summary>
/// 网关配置
/// </summary>
public class GatewayEntity
{
    public long Id { get; set; }

    [DisplayName("服务名称")]
    [Required]
    public string? Name { get; set; }

    [DisplayName("服务Id")]
    [Required]
    [NotNull]
    public string? ClusterId { get; set; }

    [DisplayName("路由Id")]
    [NotNull]
    public string? RouteId { get; set; }

    [DisplayName("路由地址")]
    [Required]
    [NotNull]
    public string? Url { get; set; }

    [DisplayName("转发地址")]
    [Required]
    [NotNull]
    public List<string>? Hosts { get; set; }
}