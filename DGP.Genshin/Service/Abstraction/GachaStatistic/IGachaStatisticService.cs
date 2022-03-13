﻿using DGP.Genshin.DataModel.GachaStatistic;
using DGP.Genshin.MiHoYoAPI.Gacha;
using DGP.Genshin.Service.GachaStatistic;
using System;
using System.Threading.Tasks;

namespace DGP.Genshin.Service.Abstraction.GachaStatistic
{
    /// <summary>
    /// 祈愿统计服务
    /// </summary>
    public interface IGachaStatisticService
    {
        /// <summary>
        /// 异步获取抽卡统计模型
        /// </summary>
        /// <param name="gachaData">抽卡源数据</param>
        /// <param name="uid">uid</param>
        /// <returns>经过各项处理的抽卡统计模型</returns>
        Statistic GetStatistic(GachaDataCollection gachaData, string uid);

        /// <summary>
        /// 按模式异步刷新
        /// </summary>
        /// <param name="gachaData">抽卡源数据</param>
        /// <param name="mode">模式</param>
        /// <param name="progressCallback">分批获取时，每批次的回调</param>
        /// <param name="full">是否使用全量刷新，尽可能覆盖数据，在官方服务器出错后启用很有帮助</param>
        /// <returns>是否完成 , 获取的记录中的 uid 若未完成则 uid 为 <see cref="null"/></returns>
        Task<(bool isOk, string? uid)> RefreshAsync(GachaDataCollection gachaData, GachaLogUrlMode mode, Action<FetchProgress> progressCallback, bool full = false);

        /// <summary>
        /// 异步导出记录到Excel
        /// </summary>
        /// <param name="gachaData">抽卡源数据</param>
        /// <param name="uid">uid</param>
        /// <param name="path">待写入的文件路径</param>
        Task ExportDataToExcelAsync(GachaDataCollection gachaData, string uid, string path);

        /// <summary>
        /// 异步导出记录到Json
        /// </summary>
        /// <param name="gachaData">抽卡源数据</param>
        /// <param name="uid">uid</param>
        /// <param name="path">待写入的文件路径</param>
        Task ExportDataToJsonAsync(GachaDataCollection gachaData, string uid, string path);

        /// <summary>
        /// 异步从Excel导入记录
        /// </summary>
        /// <param name="gachaData"></param>
        /// <param name="path"></param>
        Task<(bool isOk, string uid)> ImportFromUIGFWAsync(GachaDataCollection gachaData, string path);

        /// <summary>
        /// 异步从Json导入记录
        /// </summary>
        /// <param name="gachaData"></param>
        /// <param name="path"></param>
        Task<(bool isOk, string uid)> ImportFromUIGFJAsync(GachaDataCollection gachaData, string path);

        /// <summary>
        /// 加载本地储存的记录数据
        /// </summary>
        /// <param name="gachaData"></param>
        Task LoadLocalGachaDataAsync(GachaDataCollection gachaData);
    }
}