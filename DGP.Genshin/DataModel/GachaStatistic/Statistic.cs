﻿using DGP.Genshin.DataModel.GachaStatistic.Banner;
using DGP.Genshin.DataModel.GachaStatistic.Item;
using System.Collections.Generic;

namespace DGP.Genshin.DataModel.GachaStatistic
{
    /// <summary>
    /// 统计信息
    /// </summary>
    public class Statistic
    {
        public string? Uid { get; set; }
        public StatisticBanner? Permanent { get; set; }
        public StatisticBanner? CharacterEvent { get; set; }
        public StatisticBanner? WeaponEvent { get; set; }
        public List<StatisticItem>? Characters5 { get; set; }
        public List<StatisticItem>? Characters4 { get; set; }
        public List<StatisticItem>? Weapons5 { get; set; }
        public List<StatisticItem>? Weapons4 { get; set; }
        public List<StatisticItem>? Weapons3 { get; set; }
        public List<SpecificBanner>? SpecificBanners { get; set; }
    }
}
