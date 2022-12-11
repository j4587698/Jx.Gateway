using BootstrapBlazor.Components;
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
            if (Constants.SelectItems.ContainsKey(key))
            {
                items = Constants.SelectItems[key];
            }
            return items;
        }
    }
}
