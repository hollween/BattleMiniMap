﻿using MissionSharedLibrary.Config;
using System;
using System.IO;
using BattleMiniMap.View.Map;
using BattleMiniMap.View.MapTerrain;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade;

namespace BattleMiniMap.Config
{

    public class BattleMiniMapConfig : MissionConfigBase<BattleMiniMapConfig>
    {
        protected static Version BinaryVersion => new Version(1, 0);
        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool ShowMap { get; set; } = true;

        public float Resolution { get; set; } = 1;
        public int WidgetWidth { get; set; } = 400;

        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;

        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;

        public int PositionX { get; set; } = 10;

        public int PositionY { get; set; } = 10;

        public float Opacity { get; set; } = 0.5f;

        public bool ExcludeUnwalkableTerrain { get; set; } = false;

        protected override string SaveName { get; } = Path.Combine(ConfigPath.ConfigDir,
            BattleMiniMapSubModule.ModuleId, nameof(BattleMiniMapConfig) + ".xml");

        protected override void CopyFrom(BattleMiniMapConfig other)
        {
            ConfigVersion = other.ConfigVersion;
            ShowMap = other.ShowMap;
            Resolution = other.Resolution;
            WidgetWidth = other.WidgetWidth;
            HorizontalAlignment = other.HorizontalAlignment;
            VerticalAlignment = other.VerticalAlignment;
            PositionX = other.PositionX;
            PositionY = other.PositionY;
            Opacity = other.Opacity;
            ExcludeUnwalkableTerrain = other.ExcludeUnwalkableTerrain;
        }

        protected override void UpgradeToCurrentVersion()
        {
        }

        public static void OnMenuClosed()
        {
            if (Math.Abs(MiniMap.Instance.Resolution - Get().Resolution) > 0.001f)
                MiniMap.Instance.UpdateMapImage(Mission.Current);
            Get().Serialize();
        }
    }
}