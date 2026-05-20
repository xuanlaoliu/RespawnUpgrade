# RespawnUpgrade

[![EXILED](https://img.shields.io/badge/EXILED-9.x-blue)](https://github.com/Exiled-Team/EXILED)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

## 中文说明

### 概述

**RespawnUpgrade** 是一个基于 [EXILED](https://github.com/Exiled-Team/EXILED) 框架的 SCP: Secret Laboratory 服务器插件。当混沌或九尾狐重生波次触发时，它会自动升级在线玩家角色、强制重生观战玩家，并在指定坐标投放支援装备。

### 功能

1. **角色晋升** — 重生波次触发时，立即将全局所有符合条件的玩家晋升：
   - 混沌掠夺者 (ChaosMarauder) → 混沌征召兵 (ChaosConscript)
   - 九尾狐列兵 (NtfPrivate) → 九尾狐中士 (NtfSergeant)
2. **观战玩家强制重生** — 波次触发 5 秒后，将所有观战 (Spectator) 玩家设为对应阵营角色：
   - 混沌波 → 混沌征召兵
   - 九尾波 → 九尾狐中士
3. **装备空投** — 每次波次立即在两个坐标点各生成一套装备（共两套），每套包含：
   - 4x 重型护甲 (ArmorHeavy)
   - 4x E11-SR 步枪 (GunE11SR)
   - 1x Logicer 机枪 (GunLogicer)

### 装备投放坐标

| 点位 | 坐标 |
|------|------|
| 1 | `(134.933, 297.552, -43.236)` |
| 2 | `(0.614, 302.5, -39.853)` |

### 安装

1. 确保服务器已安装 EXILED 9.x
2. 从 [Releases](https://github.com/xuanlaoliu/RespawnUpgrade/releases) 下载 `RespawnUpgrade.dll`
3. 将 `RespawnUpgrade.dll` 放入 `EXILED/Plugins/` 目录
4. 重启服务器或使用 EXILED 重载插件

### 配置

首次运行后会在 `EXILED/Configs/` 下生成 `端口号-RespawnUpgrade.yml`：

```yaml
# 是否启用本插件
is_enabled: true
# 是否输出调试日志
debug: false
```

### 自行编译

```bash
git clone https://github.com/MMK-208/RespawnUpgrade.git
cd RespawnUpgrade
dotnet build
```

需要将 `_extracted_exiled` 和 `_extracted_labapi` 依赖目录放置在项目同级目录下。

---

## English Description

### Overview

**RespawnUpgrade** is an [EXILED](https://github.com/Exiled-Team/EXILED) plugin for SCP: Secret Laboratory. When a Chaos Insurgency or Nine-Tailed Fox respawn wave triggers, it automatically upgrades online player roles, force-spawns spectators, and drops supply crates at fixed coordinates.

### Features

1. **Role Promotion** — Immediately upgrades all eligible players server-wide when a wave spawns:
   - ChaosMarauder → ChaosConscript
   - NtfPrivate → NtfSergeant
2. **Spectator Force-Respawn** — 5 seconds after the wave, all Spectator players are set to the corresponding faction role:
   - Chaos wave → ChaosConscript
   - NTF wave → NtfSergeant
3. **Supply Drop** — Spawns two sets of equipment at two fixed locations on every eligible wave, each containing:
   - 4x Heavy Armor
   - 4x E11-SR Rifle
   - 1x Logicer Machine Gun

### Drop Coordinates

| Location | Position |
|----------|----------|
| 1 | `(134.933, 297.552, -43.236)` |
| 2 | `(0.614, 302.5, -39.853)` |

### Installation

1. Make sure your server runs EXILED 9.x
2. Download `RespawnUpgrade.dll` from [Releases](https://github.com/xuanlaoliu/RespawnUpgrade/releases)
3. Place the DLL into `EXILED/Plugins/`
4. Restart the server or reload plugins via EXILED

### Configuration

A config file `port-RespawnUpgrade.yml` will be generated in `EXILED/Configs/` on first run:

```yaml
is_enabled: true
debug: false
```

### Building from Source

```bash
git clone https://github.com/MMK-208/RespawnUpgrade.git
cd RespawnUpgrade
dotnet build
```

The `_extracted_exiled` and `_extracted_labapi` dependency directories must be placed next to the project folder.

---

## License

[MIT](LICENSE) — feel free to use, modify, and distribute.
