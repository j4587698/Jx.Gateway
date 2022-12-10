using BootstrapBlazor.Components;

namespace Jx.Gateway.Services
{
    public class LookupService : ILookupService
    {
        public IEnumerable<SelectedItem>? GetItemsByKey(string? key)
        {
            IEnumerable<SelectedItem>? items = null;
            switch (key)
            {
                case "docker.state":
                    items = new List<SelectedItem>()
                    {
                        new() { Value = "created", Text = "已创建" },
                        new() { Value = "restarting", Text = "重启中" },
                        new() { Value = "running", Text = "运行中" },
                        new() { Value = "removing", Text = "迁移中" },
                        new() { Value = "paused", Text = "已暂停" },
                        new() { Value = "exited", Text = "已停止" },
                        new() { Value = "dead", Text = "已死亡" },
                    };
                    break;
            }
            return items;
        }
    }
}
