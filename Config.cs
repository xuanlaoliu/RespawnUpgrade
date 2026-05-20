using Exiled.API.Interfaces;
using System.ComponentModel;

namespace RespawnUpgrade
{
    public class Config : IConfig
    {
        [Description("是否启用本插件")]
        public bool IsEnabled { get; set; } = true;

        [Description("是否输出调试日志到控制台")]
        public bool Debug { get; set; } = false;
    }
}