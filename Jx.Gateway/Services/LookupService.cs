using BootstrapBlazor.Components;
using Jx.Gateway.Enums;
using Jx.Toolbox.Extensions;
using Constants = Jx.Gateway.Utils.Constants;

namespace Jx.Gateway.Services
{
    public class LookupService : ILookupService
    {
        public IEnumerable<SelectedItem>? GetItemsByKey(string? key)
        {
            IEnumerable<SelectedItem>? items = null;
            if (key.IsNullOrEmpty())
            {
                return null;
            }

            items = key switch
            {
                "docker.state" => typeof(DockerState).ToSelectList(),
                _ => items
            };
            return items;
        }
    }
}
