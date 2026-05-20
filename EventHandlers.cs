using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using System;
using System.Linq;
using UnityEngine;

namespace RespawnUpgrade
{
    public class EventHandlers
    {
        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            try
            {
                SpawnableFaction faction = ev.Wave.SpawnableFaction;

                bool isChaos = IsChaosWave(faction);
                bool isNtf = IsNtfWave(faction);

                if (!isChaos && !isNtf)
                    return;

                Log.Info($"[RespawnUpgrade] 重生波次触发 - 阵营: {faction}");

                UpgradeRolesAllPlayers(isChaos);

                SpawnEquipmentAtBothLocations();

                Timing.CallDelayed(5f, () =>
                {
                    try
                    {
                        UpgradeSpectatorPlayers(isChaos);
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"[RespawnUpgrade] 延迟执行阴间玩家升级时发生异常: {ex}");
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error($"[RespawnUpgrade] OnRespawningTeam 执行时发生未预期错误: {ex}");
            }
        }

        private bool IsChaosWave(SpawnableFaction faction)
        {
            return faction == SpawnableFaction.ChaosWave || faction == SpawnableFaction.ChaosMiniWave;
        }

        private bool IsNtfWave(SpawnableFaction faction)
        {
            return faction == SpawnableFaction.NtfWave || faction == SpawnableFaction.NtfMiniWave;
        }

        private void UpgradeRolesAllPlayers(bool isChaos)
        {
            RoleTypeId sourceRole = isChaos ? RoleTypeId.ChaosMarauder : RoleTypeId.NtfPrivate;
            RoleTypeId targetRole = isChaos ? RoleTypeId.ChaosConscript : RoleTypeId.NtfSergeant;

            int count = 0;

            foreach (Player player in Player.List)
            {
                if (player.Role.Type != sourceRole)
                    continue;

                try
                {
                    player.Role.Set(targetRole, SpawnReason.ForceClass);
                    count++;
                    Log.Debug($"[RespawnUpgrade] 玩家 {player.Nickname} ({player.UserId}) 角色已升级: {sourceRole} -> {targetRole}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[RespawnUpgrade] 升级玩家 {player.Nickname} 角色时出错: {ex}");
                }
            }

            Log.Info($"[RespawnUpgrade] 已完成角色升级，共处理 {count} 名玩家: {sourceRole} -> {targetRole}");
        }

        private void UpgradeSpectatorPlayers(bool isChaos)
        {
            RoleTypeId targetRole = isChaos ? RoleTypeId.ChaosConscript : RoleTypeId.NtfSergeant;

            var spectators = Player.Get(RoleTypeId.Spectator).ToList();

            if (spectators.Count == 0)
            {
                Log.Debug("[RespawnUpgrade] 当前没有阴间玩家需要处理");
                return;
            }

            Log.Info($"[RespawnUpgrade] 发现 {spectators.Count} 名阴间玩家，正在将其设置为 {targetRole}");

            foreach (Player spectator in spectators)
            {
                try
                {
                    spectator.Role.Set(targetRole, SpawnReason.ForceClass);
                    Log.Debug($"[RespawnUpgrade] 阴间玩家 {spectator.Nickname} ({spectator.UserId}) 已重生为 {targetRole}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[RespawnUpgrade] 升级阴间玩家 {spectator.Nickname} 时出错: {ex}");
                }
            }

            Log.Info($"[RespawnUpgrade] 已完成阴间玩家升级，共处理 {spectators.Count} 名玩家 -> {targetRole}");
        }

        private void SpawnEquipmentAtBothLocations()
        {
            Log.Info("[RespawnUpgrade] 开始在两个坐标点生成支援装备");

            SpawnEquipmentSet(new Vector3(134.933f, 297.552f, -43.236f));
            SpawnEquipmentSet(new Vector3(0.614f, 302.5f, -39.853f));

            Log.Info("[RespawnUpgrade] 两个坐标点的装备已全部生成完毕");
        }

        private void SpawnEquipmentSet(Vector3 position)
        {
            Log.Debug($"[RespawnUpgrade] 正在在坐标 {position} 处生成装备");

            for (int i = 0; i < 4; i++)
                Pickup.CreateAndSpawn(ItemType.ArmorHeavy, position, default, null);

            for (int i = 0; i < 4; i++)
                Pickup.CreateAndSpawn(ItemType.GunE11SR, position, default, null);

            Pickup.CreateAndSpawn(ItemType.GunLogicer, position, default, null);

            Log.Debug($"[RespawnUpgrade] 坐标 {position} 处装备已生成完成 (4x 重甲, 4x E11步枪, 1x Logicer)");
        }
    }
}