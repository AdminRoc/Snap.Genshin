﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace DGP.Genshin.Models.MiHoYo.User
{
    /// <summary>
    /// 包装用户角色列表信息
    /// </summary>
    public class UserGameRoleInfo
    {
        [JsonProperty("list")] public List<UserGameRole> List { get; set; } = new List<UserGameRole>();
    }
}