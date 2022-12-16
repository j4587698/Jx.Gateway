using System.Security.Cryptography.X509Certificates;
using BootstrapBlazor.Components;
using Docker.DotNet.Models;
using Jx.Gateway.Enums;

namespace Jx.Gateway.Utils;

public static class Constants
{

    public static string? GetChineseState(string key)
    {
        return Utility.GetDisplayName(typeof(DockerState), key);
    }
    
    public static readonly Dictionary<string, X509Certificate2> Certificates = new();
    
    public static SystemInfoResponse SystemInfo { get; set; }
    
    public const string ValidStatus = "valid";
    public const string InvalidStatus = "invalid";
    public const string PendingStatus = "pending";
}