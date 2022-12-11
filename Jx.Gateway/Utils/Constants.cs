using BootstrapBlazor.Components;
using Jx.Gateway.Enums;

namespace Jx.Gateway.Utils;

public class Constants
{
    public static readonly Dictionary<string, List<SelectedItem>> SelectItems =
        new Dictionary<string, List<SelectedItem>>()
        {
            {
                "docker.state", typeof(DockerState).ToSelectList()
            }
        };
    
    public static string? GetChineseState(string key)
    {
        return SelectItems["docker.state"]?.FirstOrDefault(x => x.Value == key)?.Text;
    }
}