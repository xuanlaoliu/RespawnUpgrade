using Exiled.API.Features;
using System;

namespace RespawnUpgrade
{
    public class Plugin : Plugin<Config>
    {
        private EventHandlers _eventHandlers;

        public static Plugin Instance { get; private set; }

        public override string Name => "RespawnUpgrade";
        public override string Prefix => "RespawnUpgrade";
        public override string Author => "MMK&早饭祥";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public override void OnEnabled()
        {
            try
            {
                Instance = this;
                _eventHandlers = new EventHandlers();

                Exiled.Events.Handlers.Server.RespawningTeam += _eventHandlers.OnRespawningTeam;

                Log.Info("[RespawnUpgrade] 插件已启用 - 正在监听重生波次事件");
            }
            catch (Exception ex)
            {
                Log.Error($"[RespawnUpgrade] OnEnabled 初始化失败: {ex}");
            }
        }

        public override void OnDisabled()
        {
            try
            {
                if (_eventHandlers != null)
                {
                    Exiled.Events.Handlers.Server.RespawningTeam -= _eventHandlers.OnRespawningTeam;
                    _eventHandlers = null;
                }

                Instance = null;

                Log.Info("[RespawnUpgrade] 插件已禁用 - 已注销所有事件");
            }
            catch (Exception ex)
            {
                Log.Error($"[RespawnUpgrade] OnDisabled 清理失败: {ex}");
            }
        }
    }
}