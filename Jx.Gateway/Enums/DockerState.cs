using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Jx.Gateway.Enums;


public enum DockerState
{
    [Display(Name = "已创建")]
    created,
    [Display(Name = "重启中")]
    restarting,
    [Display(Name = "运行中")]
    running,
    [Display(Name = "迁移中")]
    removing,
    [Display(Name = "已暂停")]
    paused,
    [Display(Name = "已停止")]
    exited,
    [Display(Name = "已死亡")]
    dead
}